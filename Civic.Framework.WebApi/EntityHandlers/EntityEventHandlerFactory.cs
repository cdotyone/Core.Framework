using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SimpleInjector;

namespace Civic.Framework.WebApi
{
    public class EntityEventHandlerFactory : IEntityEventHandlerFactory
    {
        readonly Container _container;
        private const string AllClasses = "*";

        private static readonly ConcurrentDictionary<EntityEventType,ConcurrentDictionary<string,ConcurrentBag<IEntityEventHandler>>> _producers = 
            new ConcurrentDictionary<EntityEventType, ConcurrentDictionary<string, ConcurrentBag<IEntityEventHandler>>>();
            
        public EntityEventHandlerFactory(Container container)
        {
            _container = container;

            foreach (EntityEventType val in Enum.GetValues(typeof(EntityEventType)))
            {
                _producers[val] = new ConcurrentDictionary<string, ConcurrentBag<IEntityEventHandler>>(StringComparer.OrdinalIgnoreCase);
            }
        }

        public void Register<THandler>(IEntityInfo info, THandler producerInstance)
            where THandler : class, IEntityEventHandler
        {
            var producer = (Lifestyle.Singleton).CreateProducer<IEntityEventHandler, THandler>(_container);

            var handlers = producerInstance.Handlers;

            foreach (EntityEventType val in Enum.GetValues(typeof(EntityEventType)))
            {
                if (handlers.HasFlag(val))
                {
                    var dict = _producers[val];
                    if(!dict.ContainsKey(info.Name))
                        dict[info.Name] = new ConcurrentBag<IEntityEventHandler>();

                    dict[info.Name].Add(producerInstance);
                }
            }
        }

        public void Register<THandler>(THandler producerInstance)
            where THandler : class, IEntityEventHandler
        {
            var producer = (Lifestyle.Singleton).CreateProducer<IEntityEventHandler, THandler>(_container);
            var handlers = producerInstance.Handlers;

            foreach (EntityEventType val in Enum.GetValues(typeof(EntityEventType)))
            {
                if (handlers.HasFlag(val))
                {
                    var dict = _producers[val];
                    if (!dict.ContainsKey(AllClasses))
                        dict[AllClasses] = new ConcurrentBag<IEntityEventHandler>();

                    dict[AllClasses].Add(producerInstance);
                }
            }
        }

        public bool OnAddBefore<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            var retValue = true;

            var handlers = _producers[EntityEventType.AddBefore];
            var allTypes = handlers.ContainsKey(AllClasses);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnAddBefore(context, info, entity))
                    {
                        retValue = false;
                    }
                }
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnAddBefore(context, info, entity))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnAddAfter<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity 
        {
            var retValue = true;

            var handlers = _producers[EntityEventType.AddAfter];
            var allTypes = handlers.ContainsKey(AllClasses);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnAddAfter(context, info, entity))
                    {
                        retValue = false;
                    }
                }
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnAddAfter(context, info, entity))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnModifyBefore<T>(IEntityRequestContext context, IEntityInfo info, T before, T after) where T : class, IEntityIdentity
        {
            var retValue = true;

            var handlers = _producers[EntityEventType.ModifyBefore];
            var allTypes = handlers.ContainsKey(AllClasses);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnModifyBefore(context, info, before, after))
                    {
                        retValue = false;
                    }
                }
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnModifyBefore(context, info, before, after))
                    {
                        retValue = false;
                    }
                }
            }


            return retValue;
        }

        public bool OnModifyAfter<T>(IEntityRequestContext context, IEntityInfo info, T before, T after) where T : class, IEntityIdentity 
        {
            var retValue = true;

            var handlers = _producers[EntityEventType.ModifyAfter];
            var allTypes = handlers.ContainsKey(AllClasses);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }
            
            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnModifyAfter(context, info, before, after ))
                    {
                        retValue = false;
                    }
                }
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnModifyAfter(context, info, before, after))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnRemoveBefore<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity 
        {
            var retValue = true;

            var handlers = _producers[EntityEventType.RemoveBefore];
            var allTypes = handlers.ContainsKey(AllClasses);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnRemoveBefore(context, info, entity))
                    {
                        retValue = false;
                    }
                }
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnRemoveBefore(context, info, entity))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnRemoveAfter<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            var retValue = true;

            var handlers = _producers[EntityEventType.RemoveAfter];
            var allTypes = handlers.ContainsKey(AllClasses);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnRemoveAfter(context, info, entity))
                    {
                        retValue = false;
                    }
                }
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnRemoveAfter(context, info, entity))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnGetBefore(IEntityRequestContext context, IEntityInfo info)
        {
            var retValue = true;

            var handlers = _producers[EntityEventType.GetBefore];
            var allTypes = handlers.ContainsKey(AllClasses);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnGetBefore(context, info))
                    {
                        retValue = false;
                    }
                }
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnGetBefore(context, info))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnGetAfter<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            var retValue = true;

            var handlers = _producers[EntityEventType.GetAfter];
            var allTypes = handlers.ContainsKey(AllClasses);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnGetAfter(context, info, entity))
                    {
                        retValue = false;
                    }
                }
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnGetAfter(context, info, entity))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnGetPagedBefore(IEntityRequestContext context, IEntityInfo info)
        {
            var retValue = true;

            var handlers = _producers[EntityEventType.GetPagedBefore];
            var allTypes = handlers.ContainsKey(AllClasses);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnGetPagedBefore(context, info))
                    {
                        retValue = false;
                    }
                }
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnGetPagedBefore(context, info))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public IEnumerable<T> OnGetPagedAfter<T>(IEntityRequestContext context, IEntityInfo info, IEnumerable<T> list) where T : class, IEntityIdentity
        {
            var handlers = _producers[EntityEventType.GetPagedAfter];
            var allTypes = handlers.ContainsKey(AllClasses);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return list;
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    list = handler.OnGetPagedAfter(context, info, list);
                    if (list == null) return null;
                }
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    list = handler.OnGetPagedAfter(context, info, list);
                    if (list == null) return null;
                }
            }
            return list;
        }
    }
}
