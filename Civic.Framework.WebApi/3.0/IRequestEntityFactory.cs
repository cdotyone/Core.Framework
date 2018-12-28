namespace Civic.Framework.WebApi
{
    public interface IRequestEntityFactory
    {
        IEntityIdentity CreateNew(string module, string entity);
        IEntityIdentity CreateNew(IEntityInfo info);
    }
}