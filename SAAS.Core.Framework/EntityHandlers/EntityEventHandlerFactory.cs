using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SimpleInjector;

namespace SAAS.Core.Framework
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

        public bool OnAddBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            var retValue = true;

            var info = entity.GetInfo();
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
                    if (!handler.OnAddBefore(context, entity))
                    {
                        retValue = false;
                    }
                }
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnAddBefore(context, entity))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnAddAfter<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity 
        {
            var retValue = true;

            var info = entity.GetInfo();
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
                    if (!handler.OnAddAfter(context, entity))
                    {
                        retValue = false;
                    }
                }
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnAddAfter(context, entity))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnModifyBefore<T>(IEntityRequestContext context, T before, T after) where T : class, IEntityIdentity
        {
            var retValue = true;

            var info = before.GetInfo();
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
                    if (!handler.OnModifyBefore(context, before, after))
                    {
                        retValue = false;
                    }
                }
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnModifyBefore(context, before, after))
                    {
                        retValue = false;
                    }
                }
            }


            return retValue;
        }

        public bool OnModifyAfter<T>(IEntityRequestContext context, T before, T after) where T : class, IEntityIdentity 
        {
            var retValue = true;

            var info = before.GetInfo();
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
                    if (!handler.OnModifyAfter(context, before, after ))
                    {
                        retValue = false;
                    }
                }
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnModifyAfter(context, before, after))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnRemoveBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity 
        {
            var retValue = true;

            var info = entity.GetInfo();
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
                    if (!handler.OnRemoveBefore(context, entity))
                    {
                        retValue = false;
                    }
                }
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnRemoveBefore(context, entity))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnRemoveAfter<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            var retValue = true;

            var info = entity.GetInfo();
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
                    if (!handler.OnRemoveAfter(context, entity))
                    {
                        retValue = false;
                    }
                }
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnRemoveAfter(context, entity))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnGetBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            var retValue = true;

            var info = entity.GetInfo();
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
                    if (!handler.OnGetBefore(context, entity))
                    {
                        retValue = false;
                    }
                }
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnGetBefore(context, entity))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnGetAfter<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            var retValue = true;

            var info = entity.GetInfo();
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
                    if (!handler.OnGetAfter(context, entity))
                    {
                        retValue = false;
                    }
                }
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    if (!handler.OnGetAfter(context, entity))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public bool OnGetPagedBefore<T>(IEntityRequestContext context) where T : class, IEntityIdentity
        {
            var retValue = true;

            var info = _container.GetInstance<T>().GetInfo();
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
                    if (!handler.OnGetPagedBefore<T>(context))
                    {
                        retValue = false;
                    }
                }
            }

            if (targetTypes)
            {
                foreach (var handler in handlers[info.Name])
                {
                    if (!handler.OnGetPagedBefore<T>(context))
                    {
                        retValue = false;
                    }
                }
            }

            return retValue;
        }

        public IEnumerable<T> OnGetPagedAfter<T>(IEntityRequestContext context, IEnumerable<T> list) where T : class, IEntityIdentity
        {
            var info = _container.GetInstance<T>().GetInfo();
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
                    list = handler.OnGetPagedAfter(context, list);
                    if (list == null) return null;
                }
            }

            if (allTypes)
            {
                foreach (var handler in handlers[AllClasses])
                {
                    list = handler.OnGetPagedAfter(context, list);
                    if (list == null) return null;
                }
            }
            return list;
        }
    }
}
