namespace SAAS.Core.Framework
{
    public interface IEntityCreateFactory
    {
        IEntityIdentity CreateNew(IEntityInfo info);
        IEntityIdentity CreateNew(string module, string entity);
    }
}