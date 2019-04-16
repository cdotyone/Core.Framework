using Stack.Core.Data;

namespace Stack.Core.Framework
{
    public class SqlOperation : IEntityOperation
    {
        public EntityOperationType Type { get; set; }

        public IEntityIdentity Before { get; set; }

        public IEntityIdentity After { get; set; }

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