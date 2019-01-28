using System;
using System.Collections.Concurrent;
using SimpleInjector;

namespace SAAS.Core.Framework
{
    public class EntityCreateFactory : IEntityCreateFactory
    {
        readonly Container _container;
        private static readonly ConcurrentDictionary<string, InstanceProducer<IEntityIdentity>> _producers = new ConcurrentDictionary<string, InstanceProducer<IEntityIdentity>>(StringComparer.OrdinalIgnoreCase);
        private static readonly ConcurrentDictionary<string, InstanceProducer> _facadeProducers = new ConcurrentDictionary<string, InstanceProducer>();

        public EntityCreateFactory(Container container)
        {
            _container = container;
        }

        IEntityIdentity IEntityCreateFactory.CreateNew(IEntityInfo info) => _producers[info.Name].GetInstance();
        IEntityIdentity IEntityCreateFactory.CreateNew(string module, string entity) => _producers[module+"."+entity].GetInstance();

        public IEntityBusinessFacade<TImplementation> CreateFacade<TImplementation>() where TImplementation : class, IEntityIdentity
        {
            return _facadeProducers[typeof(TImplementation).FullName].GetInstance() as IEntityBusinessFacade<TImplementation>;
        }

        public IEntityBusinessFacade<TImplementation> CreateFacade<TImplementation>(TImplementation entity) where TImplementation : class, IEntityIdentity
        {
            return _facadeProducers[entity.GetType().FullName].GetInstance() as IEntityBusinessFacade<TImplementation>;
        }

        public void Register<TImplementation,TFacade>(Lifestyle lifestyle = null) where TImplementation : class, IEntityIdentity where TFacade : class,IEntityBusinessFacade<TImplementation>
        {
            //_container.Register<TService, TImplementation>(Lifestyle.Transient);
            var producer = (lifestyle ?? _container.Options.DefaultLifestyle).CreateProducer<IEntityIdentity, TImplementation>(_container);

            var facadeProducer = (Lifestyle.Singleton).CreateProducer<IEntityBusinessFacade<TImplementation>, TFacade>(_container);

            var info = PropertyMapper.GetInfo<TImplementation>();
            PropertyMapper.Map<TImplementation>(info);

            _facadeProducers[typeof(TImplementation).FullName] = facadeProducer;
            _producers[info.Name] = producer;
        }
    }
}
