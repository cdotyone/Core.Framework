﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SimpleInjector;

namespace SAAS.Core.Framework
{
    public class EntityCreateFactory : IEntityCreateFactory
    {
        readonly Container _container;
        private static ConcurrentDictionary<string, InstanceProducer<IEntityIdentity>> _producers = new ConcurrentDictionary<string, InstanceProducer<IEntityIdentity>>(StringComparer.OrdinalIgnoreCase);

        private static ConcurrentDictionary<string, IEntityInfo> _info = new ConcurrentDictionary<string, IEntityInfo>(StringComparer.OrdinalIgnoreCase);

        public EntityCreateFactory(Container container)
        {
            _container = container;
        }

        public static IEntityInfo GetInfo<T>(T entity)
        {
            return _info[typeof(T).Name];
        }

        public static IEntityInfo GetInfo(string name)
        {
            return _info[name];
        }

        IEntityIdentity IEntityCreateFactory.CreateNew(IEntityInfo info) => _producers[info.Name].GetInstance();
        IEntityIdentity IEntityCreateFactory.CreateNew(string module, string entity) => _producers[module+"."+entity].GetInstance();

        public void Register<TImplementation>(IEntityInfo info, Lifestyle lifestyle = null) where TImplementation : class, IEntityIdentity
        {
            //_container.Register<TService, TImplementation>(Lifestyle.Transient);
            var producer = (lifestyle ?? _container.Options.DefaultLifestyle).CreateProducer<IEntityIdentity, TImplementation>(_container);
            
            PropertyMapper.Map<TImplementation>(info);

            _info[typeof(TImplementation).Name] = info;
            _producers[info.Name] = producer;
        }
    }
}
