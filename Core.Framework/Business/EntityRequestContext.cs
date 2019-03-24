using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Core.Framework
{
    public class EntityRequestContext : IEntityRequestContext
    {
        private List<IEntityOperation> _operations;

        public ClaimsPrincipal Who { get; set; }

        public string TransactionUID { get; set; } = string.Empty.InsureUID();

        public EntityEventType IgnoreHandlers { get; set; }

        public string Expand { get; set; }

        public List<IEntityOperation> Operations
        {
            get { return _operations ?? (_operations = new List<IEntityOperation>()); }
        }

        public void Commit()
        {
            foreach (var operation in Operations)
            {
                operation.Commit();
            }
        }

        public void Rollback()
        {
            foreach (var operation in Operations)
            {
                operation.Rollback();
            }
        }
    }
}
