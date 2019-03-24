using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SAAS.Core.Framework
{
    public class EntityEventHandlerFactory : IEntityEventHandlerFactory
    {
        private const string ALL_CLASSES = "*";

        private static readonly ConcurrentDictionary<EntityEventType,ConcurrentDictionary<string,ConcurrentBag<IEntityEventHandler>>> _producers = 
            new ConcurrentDictionary<EntityEventType, ConcurrentDictionary<string, ConcurrentBag<IEntityEventHandler>>>();
            
        public EntityEventHandlerFactory()
        {
            foreach (EntityEventType val in Enum.GetValues(typeof(EntityEventType)))
            {
                _producers[val] = new ConcurrentDictionary<string, ConcurrentBag<IEntityEventHandler>>(StringComparer.OrdinalIgnoreCase);
            }
        }

        public void Register<THandler>(string name, THandler producerInstance)
            where THandler : class, IEntityEventHandler
        {
            var handlers = producerInstance.Handlers;

            foreach (EntityEventType val in Enum.GetValues(typeof(EntityEventType)))
            {
                if (handlers.HasFlag(val))
                {
                    var dict = _producers[val];
                    if(!dict.ContainsKey(name))
                        dict[name] = new ConcurrentBag<IEntityEventHandler>();

                    dict[name].Add(producerInstance);
                }
            }
        }

        public void Register<THandler>(THandler producerInstance) where THandler : class, IEntityEventHandler
        {
            var handlers = producerInstance.Handlers;

            foreach (EntityEventType val in Enum.GetValues(typeof(EntityEventType)))
            {
                if (handlers.HasFlag(val))
                {
                    var dict = _producers[val];
                    if (!dict.ContainsKey(ALL_CLASSES))
                        dict[ALL_CLASSES] = new ConcurrentBag<IEntityEventHandler>();

                    dict[ALL_CLASSES].Add(producerInstance);
                }
            }
        }

        public bool OnAddBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            var retValue = true;

            var info = EntityInfoManager.GetInfo(entity);
            var handlers = _producers[EntityEventType.AddBefore];
            var allTypes = handlers.ContainsKey(ALL_CLASSES);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (allTypes)
            {
                foreach (var handler in handlers[ALL_CLASSES])
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

            var info = EntityInfoManager.GetInfo(entity);
            var handlers = _producers[EntityEventType.AddAfter];
            var allTypes = handlers.ContainsKey(ALL_CLASSES);
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
                foreach (var handler in handlers[ALL_CLASSES])
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

            var info = EntityInfoManager.GetInfo(before);
            var handlers = _producers[EntityEventType.ModifyBefore];
            var allTypes = handlers.ContainsKey(ALL_CLASSES);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (allTypes)
            {
                foreach (var handler in handlers[ALL_CLASSES])
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

            var info = EntityInfoManager.GetInfo(before);
            var handlers = _producers[EntityEventType.ModifyAfter];
            var allTypes = handlers.ContainsKey(ALL_CLASSES);
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
                foreach (var handler in handlers[ALL_CLASSES])
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

            var info = EntityInfoManager.GetInfo(entity);
            var handlers = _producers[EntityEventType.RemoveBefore];
            var allTypes = handlers.ContainsKey(ALL_CLASSES);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (allTypes)
            {
                foreach (var handler in handlers[ALL_CLASSES])
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

            var info = EntityInfoManager.GetInfo(entity);
            var handlers = _producers[EntityEventType.RemoveAfter];
            var allTypes = handlers.ContainsKey(ALL_CLASSES);
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
                foreach (var handler in handlers[ALL_CLASSES])
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

            var info = EntityInfoManager.GetInfo(entity);
            var handlers = _producers[EntityEventType.GetBefore];
            var allTypes = handlers.ContainsKey(ALL_CLASSES);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (allTypes)
            {
                foreach (var handler in handlers[ALL_CLASSES])
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

            var info = EntityInfoManager.GetInfo(entity);
            var handlers = _producers[EntityEventType.GetAfter];
            var allTypes = handlers.ContainsKey(ALL_CLASSES);
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
                foreach (var handler in handlers[ALL_CLASSES])
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

            var info = EntityInfoManager.GetInfo(typeof(T));
            var handlers = _producers[EntityEventType.GetPagedBefore];
            var allTypes = handlers.ContainsKey(ALL_CLASSES);
            var targetTypes = handlers.ContainsKey(info.Name);

            if (!allTypes && !targetTypes)
            {
                return true;
            }

            if (allTypes)
            {
                foreach (var handler in handlers[ALL_CLASSES])
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
            var info = EntityInfoManager.GetInfo(typeof(T));
            var handlers = _producers[EntityEventType.GetPagedAfter];
            var allTypes = handlers.ContainsKey(ALL_CLASSES);
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
                foreach (var handler in handlers[ALL_CLASSES])
                {
                    list = handler.OnGetPagedAfter(context, list);
                    if (list == null) return null;
                }
            }
            return list;
        }
    }
}
