using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using Civic.Core.Data;
using Civic.Core.Logging;
using SAAS.Core.Framework.OData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public static T Get<T>(Container container, ClaimsPrincipal who, T item, IDBConnection database) where T : class,IEntityIdentity
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(SqlQuery), "Get"))
            {
                var info = item.GetInfo();
                var entityID = item._key;

                if (!AuthorizationHelper.CanView(who, info)) throw new UnauthorizedAccessException();

                try
                {
                    var view = $"[{info.Module}].[VW_{info.Entity.ToUpperInvariant()}]";

                    var keyValues = entityID.Split('|').ToList();

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

                        command.ExecuteReader(dataReader =>
                        {
                            if (PopulateEntity(entity, dataReader, info.Properties, true))
                            {
                                var json = JsonConvert.SerializeObject(entity);
                                JsonConvert.PopulateObject(json, item);
                                entity = new Entity();
                            }
                            else item = null;
                        });
                    }

                    return item;
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }
            }

            return null;
        }

        public static IEnumerable<T> GetPaged<T>(Container container, ClaimsPrincipal who, IEntityInfo info, int skip, ref int count, bool retCount, string filterBy, string orderBy, IDBConnection database) where T : class,IEntityIdentity
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(SqlQuery), "GetPaged"))
            {
                if (!AuthorizationHelper.CanView(who, info)) throw new UnauthorizedAccessException();

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
                            while (PopulateEntity(entity, dataReader, info.Properties, true))
                            {
                                var json = JsonConvert.SerializeObject(entity);
                                T item = factory.CreateNew(info) as T;
                                JsonConvert.PopulateObject(json,item);
                                list.Add(item);
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

        public static bool PopulateEntity(Entity entity, IDataReader dataReader, Dictionary<string, IEntityPropertyInfo> propertyNames, bool stripLeadUnderscore)
        {
            if (dataReader == null || !dataReader.Read()) return false;

            entity.Properties = new Dictionary<string, object>();

            for (int i = 0, l = dataReader.FieldCount; i < l; i++)
            {
                if (dataReader.IsDBNull(i))
                    continue;

                try
                {
                    var name = dataReader.GetName(i);
                    string type;
                    if (propertyNames.ContainsKey(name))
                    {
                        IEntityPropertyInfo property = propertyNames[name];
                        name = property.Name;
                        type = property.Type.ToLower();
                    }
                    else type = dataReader.GetDataTypeName(i).ToLower();
                    if (stripLeadUnderscore) name = name.TrimStart(new[] { '_' });

                    switch (type)
                    {
                        case "double":
                            entity.Properties.Add(name, dataReader.GetDouble(i));
                            break;
                        case "uniqueidentifier":
                            entity.Properties.Add(name, Guid.Parse(dataReader.GetString(i)));
                            break;
                        case "money":
                        case "decimal":
                            entity.Properties.Add(name, dataReader.GetDecimal(i));
                            break;
                        case "time":
                        case "date":
                        case "smalldatetime":
                        case "datetime":
                        case "datetime2":
                            entity.Properties.Add(name, dataReader.GetDateTime(i).FromDB());
                            break;
                        case "smallint":
                            entity.Properties.Add(name, dataReader.GetInt16(i));
                            break;
                        case "int":
                            entity.Properties.Add(name, dataReader.GetInt32(i));
                            break;
                        case "bigint":
                            entity.Properties.Add(name, dataReader.GetInt64(i));
                            break;
                        case "bit":
                            entity.Properties.Add(name, dataReader.GetBoolean(i));
                            break;
                        default:
                            var value = dataReader.GetValue(i).ToString();

                            switch (type)
                            {
                                case "json":
                                    entity.Properties.Add(name, JsonConvert.DeserializeObject<JObject>(value));
                                    break;
                                case "boolean":
                                    entity.Properties.Add(name, value == "1" || value.ToLower() == "true");
                                    break;
                                case "smallint":
                                    entity.Properties.Add(name, Int16.Parse(value));
                                    break;
                                case "money":
                                    entity.Properties.Add(name, decimal.Parse(value));
                                    break;
                                case "float":
                                case "decimal":
                                    entity.Properties.Add(name, float.Parse(value));
                                    break;
                                case "int":
                                    entity.Properties.Add(name, Int32.Parse(value));
                                    break;
                                case "bigint":
                                    entity.Properties.Add(name, Int64.Parse(value));
                                    break;
                                case "datetime":
                                    entity.Properties.Add(name, DateTime.Parse(value).FromDB());
                                    break;
                                default:
                                    entity.Properties.Add(name, value);
                                    break;
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // intentionally bury exceptions
                    Logger.HandleException(LoggingBoundaries.DataLayer, ex);
                }
            }

            return true;
        }

        public static bool PopulateEntity<T>(IEntityRequestContext context, T item, IDataReader dataReader)  where T : class, IEntityIdentity
        {
            if (dataReader == null || !dataReader.Read()) return false;

            var entity = new Entity();
            if (!PopulateEntity(entity, dataReader, item.GetInfo().Properties, false))
                return false;

            var json = JsonConvert.SerializeObject(entity);
            JsonConvert.PopulateObject(json, item);

            return true;
        }
    }
}
