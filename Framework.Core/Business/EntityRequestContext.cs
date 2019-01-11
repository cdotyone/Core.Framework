using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Framework.Core
{
    public class EntityRequestContext : IEntityRequestContext
    {
        private List<IEntityOperation> _operations;

        public ClaimsPrincipal Who { get; set; }

        public string TransactionUID { get; set; } = string.Empty.InsureUID();

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
