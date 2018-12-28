using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SimpleInjector;

namespace Civic.Framework.WebApi
{
    public class RequestEntityFactory : IRequestEntityFactory
    {
        readonly Container _container;
        private static ConcurrentDictionary<string, InstanceProducer<IEntityIdentity>> _producers = new ConcurrentDictionary<string, InstanceProducer<IEntityIdentity>>(StringComparer.OrdinalIgnoreCase);

        public RequestEntityFactory(Container container)
        {
            _container = container;
        }

        IEntityIdentity IRequestEntityFactory.CreateNew(string module,string entity) => _producers[module+"."+entity].GetInstance();
        IEntityIdentity IRequestEntityFactory.CreateNew(IEntityInfo info) => _producers[info.Name].GetInstance();

        public void Register<TImplementation>(IEntityInfo info, Lifestyle lifestyle = null)
            where TImplementation : class, IEntityIdentity
        {
            var producer = (lifestyle ?? _container.Options.DefaultLifestyle).CreateProducer<IEntityIdentity, TImplementation>(_container);

            _producers[info.Name] = producer;
        }
    }
}
