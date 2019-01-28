using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Civic.Core.Data;
using Civic.Core.Logging;
using SAAS.Core.Framework.OData;
using SimpleInjector;

namespace SAAS.Core.Framework
{
    public class SqlQuery
    {
        public static IDBConnection GetConnection(string dbCode, EntityOperationType type, IEntityIdentity before, IEntityIdentity after, IEntityRequestContext context)
        {
            dbCode = DataConfig.Current.GetConnectionName(dbCode);

            IDBConnection connection = null;
            foreach (var operation in context.Operations)
            {
                var sqlOperation = operation as SqlOperation;
                if (sqlOperation == null) continue;
                if (sqlOperation.DbCode == dbCode)
                {
                    connection = sqlOperation.Connection;
                }
            }

            if (connection == null && context.Who != null)
            {
                connection =  DatabaseFactory.CreateDatabase(dbCode).AddClaimsDefaults(context.Who);
            }
            if(connection==null) connection= DatabaseFactory.CreateDatabase(dbCode);

            if (type != EntityOperationType.Get)
            {
                if (!connection.IsInTransaction)
                {
                    connection.BeginTrans();
                }

                context.Operations.Add(new SqlOperation
                {
                    DbCode = dbCode,
                    Type = type,
                    Before = before,
                    After = after,
                    Connection = connection
                });
            }

            return connection;
        }

        public static T Get<T>(Container container, IEntityRequestContext context, IEntityInfo info, string primaryKey, IDBConnection database) where T : class,IEntityIdentity
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(SqlQuery), "Get"))
            {
                try
                {
                    var view = $"[{info.Module}].[VW_{info.Entity.ToUpperInvariant()}]";

                    var keyValues = primaryKey.Split('|').ToList();

                    var keys = new List<string>();
                    var parameters = new List<DbParameter>();
                    var idx = 0;
                    foreach (var property in info.Properties)
                    {
                        idx++;
                        if (!property.Value.IsKey) continue;

                        keys.Add($"{property.Key} = @val{idx}");
                        parameters.Add(database.CreateParameter($"@val{idx}", keyValues[0]));
                    }

                    var where = string.Join(" AND ", keys);

                    using (database)
                    {
                        var command = database.CreateCommand($"SELECT * FROM {view} WHERE {where}", CommandType.Text);
                        foreach (var parameter in parameters)
                        {
                            command.AddParameter(parameter);
                        }

                        var entity = new Entity();

                        var factory = container.GetInstance<IEntityCreateFactory>();
                        T item = factory.CreateNew(info) as T;

                        command.ExecuteReader(dataReader =>
                        {
                            if (!PopulateEntity(item, info, dataReader, true))
                                item = null;
                        });

                        return item;
                    }
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }
            }

            return null;
        }

        public static IEnumerable<T> GetPaged<T>(Container container, IEntityRequestContext context, IEntityInfo info, int skip, ref int count, bool retCount, string filterBy, string orderBy, IDBConnection database) where T : class,IEntityIdentity
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(SqlQuery), "GetPaged"))
            {
                if (!AuthorizationHelper.CanView(context.Who, info)) throw new UnauthorizedAccessException();

                try
                {
                    var view = $"[{info.Module}].[VW_{info.Entity}]";
                    List<T> list = new List<T>();

                    using (var command = database.CreateStoredProcCommand("civic", "usp_EntityGetByFilter"))
                    {
                        string where = null;
                        if (!string.IsNullOrEmpty(filterBy))
                        {
                            where = OData3FilterBuilder.ParseExpression(command, filterBy, info.Properties.Keys.ToArray());
                        }

                        command.AddInParameter("@view", view);
                        command.AddInParameter("@skip", skip);
                        command.AddInParameter("@retCount", retCount);
                        if (!string.IsNullOrEmpty(where)) command.AddInParameter("@where", where);
                        if (!string.IsNullOrEmpty(orderBy)) command.AddInParameter("@orderBy", orderBy.Replace(", ",",").Replace(" ", "_"));
                        command.AddParameter("@count", ParameterDirection.InputOutput, count);
                        command.AddOutParameter("@debug", "");

                        var entity = new Entity();

                        var factory = container.GetInstance<IEntityCreateFactory>();

                        command.ExecuteReader(dataReader =>
                        {
                            T item = factory.CreateNew(info) as T;
                            while (PopulateEntity(item, info, dataReader, true))
                            {
                                list.Add(item);
                                item = factory.CreateNew(info) as T;
                            }
                        });

                        Logger.LogTrace(LoggingBoundaries.Database, "DYNAMIC SQL EXECUTED: {0}", command.GetOutParameter("@debug").Value.ToString());
                        if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
                    }


                    return list;
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }

            return null;
        }

        public static bool PopulateEntity<T>(T entity, IDataReader dataReader, bool stripLeadUnderscore) where T : class, IEntityIdentity
        {
            var info = PropertyMapper.GetInfo(entity);
            return PopulateEntity(entity, info, dataReader, stripLeadUnderscore);
        }

        public static bool PopulateEntity<T>(T entity, IEntityInfo info, IDataReader dataReader, bool stripLeadUnderscore) where T : class, IEntityIdentity
        {
            if (dataReader == null || !dataReader.Read()) return false;

            var extra = entity._extra ?? new Dictionary<string, object>();

            //PropertyMapper.Map<T>(info);

            var propertyNames = info.Properties;

            for (int i = 0, l = dataReader.FieldCount; i < l; i++)
            {
                if (dataReader.IsDBNull(i))
                    continue;

                try
                {
                    var name = dataReader.GetName(i);
                    string type;

                    IEntityPropertyInfo property = null;

                    if (propertyNames.ContainsKey(name))
                    {
                        property = propertyNames[name];
                        name = property.Name;
                        type = property.Type.ToLower();
                    }
                    else type = dataReader.GetDataTypeName(i).ToLower();
                    if (stripLeadUnderscore) name = name.TrimStart(new[] { '_' });

                    object value;
                    switch (type)
                    {
                        case "double":
                            value = dataReader.GetDouble(i);
                            break;
                        case "float":
                            value = dataReader.GetFloat(i);
                            break;
                        case "guid":
                        case "uniqueidentifier":
                            value = Guid.Parse(dataReader.GetString(i));
                            break;
                        case "money":
                        case "decimal":
                            value = dataReader.GetDecimal(i);
                            break;
                        case "time":
                        case "date":
                        case "smalldatetime":
                        case "datetime":
                        case "datetime2":
                            value = dataReader.GetDateTime(i);
                            break;
                        case "smallint":
                            value = dataReader.GetInt16(i);
                            break;
                        case "short":
                        case "int16":
                            value = dataReader.GetInt16(i);
                            break;
                        case "int":
                        case "int32":
                            value = dataReader.GetInt32(i);
                            break;
                        case "bigint":
                        case "int64":
                            value = dataReader.GetInt64(i);
                            break;
                        case "bit":
                            value = dataReader.GetBoolean(i);
                            break;
                        default:
                            value = dataReader.GetValue(i).ToString();
                            break;
                    }

                    if (property!=null)
                    {
                        if (property.Set != null)
                        {
                            property.Set.DynamicInvoke(entity, value);
                        }
                        else extra[name] = value;
                    } else extra[name] = value;

                }
                catch (Exception ex)
                {
                    // intentionally bury exceptions
                    Logger.HandleException(LoggingBoundaries.DataLayer, ex);
                }
            }

            return true;
        }

/*        public static bool PopulateEntity<T>(IEntityRequestContext context, T item, IDataReader dataReader)  where T : class, IEntityIdentity
        {
            if (dataReader == null || !dataReader.Read()) return false;

            var entity = new Entity();
            if (!PopulateEntity(entity, dataReader, item.GetInfo().Properties, false))
                return false;

            var json = JsonConvert.SerializeObject(entity);
            JsonConvert.PopulateObject(json, item);

            return true;
        }*/
    }
}
