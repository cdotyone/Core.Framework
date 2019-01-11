namespace Framework.Core
{
    public interface IEntityCreateFactory
    {
        IEntityIdentity CreateNew(IEntityInfo info);
        IEntityIdentity CreateNew(string module, string entity);
    }
}