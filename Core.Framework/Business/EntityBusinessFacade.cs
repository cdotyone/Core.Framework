using System;
using System.Collections.Generic;
using System.Security.Claims;
using SimpleInjector;
using Core.Logging;

namespace Core.Framework
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

        public Container Container
        {
            get { return _container; }
        }

        public virtual T Get(IEntityRequestContext context, string key)
        {
            var entity = _container.GetInstance<T>();
            entity._key = key;
            return Get(context,entity);
        }


        public virtual T Get(IEntityRequestContext context, IEntityIdentity entity)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(EntityBusinessFacade<T>), "Get"))
            {

                try
                {
                    if (!context.IgnoreHandlers.HasFlag(EntityEventType.GetBefore) && !_handlers.OnGetBefore(context, entity))
                        return null;

                    var item = _repository.Get(context, entity as T);

                    if (!context.IgnoreHandlers.HasFlag(EntityEventType.GetAfter) && !_handlers.OnGetAfter(context, item))
                        return null;

                    return item;
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
                    if (count < 0) count = 5000;

                    if (!context.IgnoreHandlers.HasFlag(EntityEventType.GetPagedBefore) && !_handlers.OnGetPagedBefore<T>(context))
                        return null;

                    var list = _repository.GetPaged(context, skip, ref count, retCount, filterBy, orderBy);

                    if (!context.IgnoreHandlers.HasFlag(EntityEventType.GetPagedAfter))
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

        public virtual void Save(IEntityRequestContext context, IEntityIdentity entity)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(EntityBusinessFacade<T>), "Save"))
            {
                var first = context.Operations.Count == 0;

                try
                {
                    var before = _repository.Get(context, entity as T);

                    if (before == null)
                    {
                        if (!context.IgnoreHandlers.HasFlag(EntityEventType.AddBefore) && !_handlers.OnAddBefore(context, entity))
                            throw new Exception("OnAddBefore handler rejected");

                        _repository.Add(context, entity as T);

                        if (!context.IgnoreHandlers.HasFlag(EntityEventType.AddAfter) && !_handlers.OnAddAfter(context, entity))
                            throw new Exception("OnAddAfter handler rejected");
                    }
                    else
                    {
                        if (!context.IgnoreHandlers.HasFlag(EntityEventType.ModifyBefore) && !_handlers.OnModifyBefore(context, before, entity))
                            throw new Exception("OnModifyBefore handler rejected");

                        _repository.Modify(context, before, entity as T);

                        if (!context.IgnoreHandlers.HasFlag(EntityEventType.ModifyAfter) && !_handlers.OnModifyAfter(context, before, entity))
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

        public virtual void Remove(IEntityRequestContext context, IEntityIdentity entity)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(EntityBusinessFacade<T>), "Remove"))
            {
                var first = context.Operations.Count == 0;

                try
                {
                    var before = _repository.Get(context, entity as T);

                    if (!context.IgnoreHandlers.HasFlag(EntityEventType.RemoveBefore) && !_handlers.OnRemoveBefore(context, before))
                        throw new Exception("OnRemoveBefore handler rejected");

                    _repository.Remove(context, before);

                    if (!context.IgnoreHandlers.HasFlag(EntityEventType.RemoveAfter) && !_handlers.OnRemoveAfter(context, before))
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

        public virtual T Get(ClaimsPrincipal who, IEntityIdentity entity)
        {
            return Get(new EntityRequestContext {Who = who}, entity);
        }

        public virtual void Save(ClaimsPrincipal who, IEntityIdentity entity)
        {
            Save(new EntityRequestContext {Who = who}, entity);
        }

        public void Remove(ClaimsPrincipal who, string key)
        {
            var entity = _container.GetInstance<T>();
            entity._key = key;
            Remove(new EntityRequestContext { Who = who }, entity);
        }

        public virtual void Remove(ClaimsPrincipal who, IEntityIdentity entity)
        {
            Remove(new EntityRequestContext {Who = who}, entity);
        }

        public virtual IEnumerable<T> GetPaged(ClaimsPrincipal who, int skip, ref int count, bool retCount, string filterBy, string orderBy, EntityEventType ignoreHandlers)
        {
            return GetPaged(new EntityRequestContext { Who = who, IgnoreHandlers = ignoreHandlers }, skip, ref count, retCount, filterBy, orderBy);
        }
        
        public T Get(ClaimsPrincipal who, string key, EntityEventType ignoreHandlers)
        {
            var entity = _container.GetInstance<T>();
            entity._key = key;
            return Get(new EntityRequestContext { Who = who, IgnoreHandlers = ignoreHandlers }, entity);
        }

        public virtual T Get(ClaimsPrincipal who, T entity, EntityEventType ignoreHandlers)
        {
            return Get(new EntityRequestContext { Who = who, IgnoreHandlers = ignoreHandlers }, entity);
        }

        public virtual void Save(ClaimsPrincipal who, T entity, EntityEventType ignoreHandlers)
        {
            Save(new EntityRequestContext { Who = who, IgnoreHandlers = ignoreHandlers }, entity);
        }

        public void Remove(ClaimsPrincipal who, string key, EntityEventType ignoreHandlers)
        {
            var entity = _container.GetInstance<T>();
            entity._key = key;
            Remove(new EntityRequestContext { Who = who, IgnoreHandlers = ignoreHandlers }, entity);
        }

        public virtual void Remove(ClaimsPrincipal who, T entity, EntityEventType ignoreHandlers)
        {
            Remove(new EntityRequestContext { Who = who, IgnoreHandlers = ignoreHandlers }, entity);
        }

    }
}

