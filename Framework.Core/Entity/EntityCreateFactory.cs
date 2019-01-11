using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SimpleInjector;

namespace Framework.Core
{
    public class EntityCreateFactory : IEntityCreateFactory
    {
        readonly Container _container;
        private static ConcurrentDictionary<string, InstanceProducer<IEntityIdentity>> _producers = new ConcurrentDictionary<string, InstanceProducer<IEntityIdentity>>(StringComparer.OrdinalIgnoreCase);

        public EntityCreateFactory(Container container)
        {
            _container = container;
        }

        IEntityIdentity IEntityCreateFactory.CreateNew(IEntityInfo info) => _producers[info.Name].GetInstance();
        IEntityIdentity IEntityCreateFactory.CreateNew(string module, string entity) => _producers[module+"."+entity].GetInstance();

        public void Register<TService, TImplementation>(IEntityInfo info, Lifestyle lifestyle = null)
            where TImplementation : class, IEntityIdentity, TService where TService : class
        {
            _container.Register<TService, TImplementation>(Lifestyle.Transient);
            var producer = (lifestyle ?? _container.Options.DefaultLifestyle).CreateProducer<IEntityIdentity, TImplementation>(_container);

            _producers[info.Name] = producer;
        }
    }
}
