using Civic.Core.Data;

namespace Civic.Framework.WebApi
{
    public class SqlOperation : IEntityOperation
    {
        public EntityOperationType Type { get; set; }

        public IEntityIdentity Entity { get; set; }

        public void Commit()
        {
            if (Connection.IsInTransaction) Connection.Commit();
        }

        public void Rollback()
        {
            if (Connection.IsInTransaction) Connection.Rollback();
        }

        public string DbCode { get; set; }

        public IDBConnection Connection { get; set; }
    }
}