namespace Civic.Framework.WebApi
{
    public interface IEntityCreateFactory
    {
        IEntityIdentity CreateNew(IEntityInfo info);
    }
}