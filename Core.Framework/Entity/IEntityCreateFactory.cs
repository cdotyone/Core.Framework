namespace Core.Framework
{
    public interface IEntityCreateFactory
    {
        IEntityIdentity CreateNew(IEntityInfo info);
        IEntityIdentity CreateNew(string module, string entity);

        IEntityBusinessFacade<TImplementation> CreateFacade<TImplementation>() where TImplementation : class, IEntityIdentity;
        IEntityBusinessFacade<TImplementation> CreateFacade<TImplementation>(TImplementation entity) where TImplementation : class, IEntityIdentity;
    }
}