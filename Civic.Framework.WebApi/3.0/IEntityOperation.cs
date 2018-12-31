namespace Civic.Framework.WebApi
{
    public interface IEntityOperation
    {
        IEntityIdentity Entity { get; set; }

        EntityOperationType Type { get; set; }

        void Commit();

        void Rollback();
    }
}