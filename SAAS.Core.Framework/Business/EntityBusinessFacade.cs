using System;
using System.Collections.Generic;
using System.Security.Claims;
using Civic.Core.Logging;
using SimpleInjector;

namespace SAAS.Core.Framework
{
    public class EntityBusinessFacade<T> : IEntityBusinessFacade<T> where T : class, IEntityIdentity
    {
        private readonly IEntityRepository<T> _repository;
        private readonly IEntityEventHandlerFactory _handlers;
        private readonly Container _container;

        public EntityBusinessFacade(Container container, IEntityRepository<T> repository, IEntityEventHandlerFactory handlers)
        {
            _container = container;
            _repository = repository;
            _handlers = handlers;
        }

        public virtual T Get(IEntityRequestContext context, string key)
        {
            var entity = _container.GetInstance<T>();
            entity._key = key;
            return Get(context,entity);
        }


        public virtual T Get(IEntityRequestContext context, T entity)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(EntityBusinessFacade<T>), "Get"))
            {

                try
                {
                    if (!_handlers.OnGetBefore(context, entity))
                        return null;

                    entity = _repository.Get(context, entity);

                    if (!_handlers.OnGetAfter(context, entity))
                        return null;

                    return entity;
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }

            return null;
        }

        public virtual IEnumerable<T> GetPaged(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(EntityBusinessFacade<T>), "GetPaged"))
            {

                try
                {
                    if (!_handlers.OnGetPagedBefore<T>(context))
                        return null;

                    var list = _repository.GetPaged(context, skip, ref count, retCount, filterBy, orderBy);

                    list = _handlers.OnGetPagedAfter(context, list);

                    return list;
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }

            return null;
        }

        public virtual void Save(IEntityRequestContext context, T entity)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(EntityBusinessFacade<T>), "Save"))
            {
                var first = context.Operations.Count == 0;

                try
                {
                    var before = _repository.Get(context, entity);

                    if (before == null)
                    {
                        if (!_handlers.OnAddBefore(context, entity))
                            throw new Exception("OnAddBefore handler rejected");

                        _repository.Add(context, entity);

                        if (!_handlers.OnAddAfter(context, entity))
                            throw new Exception("OnAddAfter handler rejected");
                    }
                    else
                    {
                        if (!_handlers.OnModifyBefore(context, before, entity))
                            throw new Exception("OnModifyBefore handler rejected");

                        _repository.Modify(context, before, entity);

                        if (!_handlers.OnModifyAfter(context, before, entity))
                            throw new Exception("OnModifyAfter handler rejected");
                    }
                }
                catch (Exception ex)
                {
                    context.Rollback();
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }
                finally
                {
                    if (first)
                    {
                        context.Commit();
                    }
                }
            }
        }

        public virtual void Remove(IEntityRequestContext context, T entity)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(EntityBusinessFacade<T>), "Remove"))
            {
                var first = context.Operations.Count == 0;

                try
                {
                    var before = _repository.Get(context, entity);

                    if (!_handlers.OnRemoveBefore(context, before))
                        throw new Exception("OnRemoveBefore handler rejected");

                    _repository.Remove(context, before);

                    if (!_handlers.OnRemoveAfter(context, before))
                        throw new Exception("OnRemoveAfter handler rejected");
                }
                catch (Exception ex)
                {
                    context.Rollback();
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }
                finally
                {
                    if (first)
                    {
                        context.Commit();
                    }
                }

            }
        }

        public virtual IEnumerable<T> GetPaged(ClaimsPrincipal who, int skip, ref int count,
            bool retCount, string filterBy, string orderBy)
        {
            return GetPaged(new EntityRequestContext {Who = who}, skip, ref count, retCount, filterBy, orderBy);
        }

        public T Get(ClaimsPrincipal who, string key)
        {
            var entity = _container.GetInstance<T>();
            entity._key = key;
            return Get(new EntityRequestContext { Who = who }, entity);
        }

        public virtual T Get(ClaimsPrincipal who, T entity)
        {
            return Get(new EntityRequestContext {Who = who}, entity);
        }

        public virtual void Save(ClaimsPrincipal who, T entity)
        {
            Save(new EntityRequestContext {Who = who}, entity);
        }

        public void Remove(ClaimsPrincipal who, string key)
        {
            var entity = _container.GetInstance<T>();
            entity._key = key;
            Remove(new EntityRequestContext { Who = who }, entity);
        }

        public virtual void Remove(ClaimsPrincipal who, T entity)
        {
            Remove(new EntityRequestContext {Who = who}, entity);
        }
    }
}

