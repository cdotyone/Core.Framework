using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using SimpleInjector;
using Core.Data;
using IDbCommand = Core.Data.IDbCommand;

namespace Core.Framework
{
    public class SqlRepository<T> : IEntityRepository<T> where T : class, IEntityIdentity
    {
        readonly Container _container;
        private readonly IEntityInfo _info;

        protected SqlRepository(Container container)
        {
            _container = container;
            _info = EntityInfoManager.GetInfo(typeof(T));
        }

        public T Get(IEntityRequestContext context, T entity)
        {
            using (var database = SqlQuery.GetConnection(_info.Module, EntityOperationType.Get, null, null, context))
            {
                Debug.Assert(database != null);

                return SqlQuery.Get<T>(_container, context, EntityInfoManager.GetInfo(entity), entity._key, database);
            }
        }

        public IEnumerable<T> GetPaged(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy)
        {
            using (var database = SqlQuery.GetConnection(_info.Module, EntityOperationType.Get, null, null, context))
            {

                Debug.Assert(database != null);

                var list = new List<T>();

                var entityList = SqlQuery.GetPaged<T>(_container, context, _info, skip, ref count, retCount, filterBy, orderBy, database);
                foreach (var entity in entityList)
                {
                    list.Add(entity);
                }

                return list;
            }
        }

        public void Add(IEntityRequestContext context, T entity)
        {
            using (var database = SqlQuery.GetConnection(_info.Module, EntityOperationType.Add, entity, null, context))
            {

                Debug.Assert(database != null);

                using (var command = database.CreateStoredProcCommand(_info.Module, $"usp_{_info.Entity}Add"))
                {
                    BuildCommand(context, entity, command, true);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Modify(IEntityRequestContext context, T before, T after)
        {
            using (var database =
                SqlQuery.GetConnection(_info.Module, EntityOperationType.Modify, before, after, context))
            {
                Debug.Assert(database != null);

                using (var command = database.CreateStoredProcCommand(_info.Module, $"usp_{_info.Entity}Modify"))
                {
                    BuildCommand(context, after, command, false);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove(IEntityRequestContext context, T entity)
        {
            using (var database = SqlQuery.GetConnection(_info.Module, EntityOperationType.Remove, entity, null, context))
            {

                Debug.Assert(database != null);

                using (var command = database.CreateStoredProcCommand(_info.Module, $"usp_{_info.Entity}Remove"))
                {
                    BuildCommand(context, entity, command, false);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void BuildCommand(IEntityRequestContext context, T entity, IDbCommand command, bool addRecord)
        {
            Debug.Assert(command != null);

            var properties = _info.Properties;
            //EntityInfoManager.Map<T>(_info);

            foreach (var property in properties)
            {
                var prop = property.Value;
                var value = prop.Get.DynamicInvoke(entity);
                if(value == null) continue;

                if (prop.Type == "json" )
                {
                    value = JsonConvert.SerializeObject(value);
                }

                if (value is DateTime)
                {
                    value = (value as DateTime?).ToDB();
                }

                if (addRecord && prop.IsIdentity.HasValue && prop.IsIdentity.Value)
                {
                    command.AddParameter("@"+prop.Name,ParameterDirection.InputOutput, value);
                }
                else
                {
                    command.AddInParameter("@" + prop.Name, value);
                }
            }
        }
    }
}

