using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using Civic.Core.Data;
using Civic.Core.Logging;
using Newtonsoft.Json;
using SimpleInjector;

namespace Civic.Framework.WebApi
{
    public class SqlQuery
    {
        public static IEntityIdentity Get(Container container, ClaimsPrincipal who, string entityID, IEntityInfo info, IDBConnection database)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(SqlQuery), "Get"))
            {

                if (!AuthorizationHelper.CanView(who, info)) throw new UnauthorizedAccessException();

                try
                {
                    IEntityIdentity item = null;

                    var view = $"[{info.Module}].[VW_{info.Entity}]";

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
                        var factory =container.GetInstance<IRequestEntityFactory>();

                        command.ExecuteReader(dataReader =>
                        {
                            while (populateEntity(entity, dataReader, info.Properties, true))
                            {
                                var json = JsonConvert.SerializeObject(entity);
                                item = factory.CreateNew(info);
                                JsonConvert.PopulateObject(json, item);
                            }
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

        public static List<IEntityIdentity> GetPaged(Container container, ClaimsPrincipal who, IEntityInfo info, int skip, ref int count, bool retCount, string filterBy, string orderBy, IDBConnection database)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(SqlQuery), "GetPaged"))
            {
                if (!AuthorizationHelper.CanView(who, info)) throw new UnauthorizedAccessException();

                try
                {
                    var view = $"[{info.Module}].[VW_{info.Entity}]";
                    List<IEntityIdentity> list = new List<IEntityIdentity>();

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
                        command.AddInParameter("@orderBy", orderBy);
                        command.AddParameter("@count", ParameterDirection.InputOutput, count);
                        command.AddOutParameter("@debug", "");

                        var entity = new Entity();

                        var factory = container.GetInstance<IRequestEntityFactory>();

                        command.ExecuteReader(dataReader =>
                        {                            
                            while (populateEntity(entity, dataReader, info.Properties, true))
                            {
                                var json = JsonConvert.SerializeObject(entity);
                                var item = factory.CreateNew(info);
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

        private static bool populateEntity(Entity entity, IDataReader dataReader, Dictionary<string, IEntityPropertyInfo> propertyNames, bool stripLeadUnderscore)
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

                    switch (dataReader.GetDataTypeName(i).ToLower())
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

    }
}
