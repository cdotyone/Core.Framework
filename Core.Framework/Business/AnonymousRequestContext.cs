using System.Collections.Generic;
using System.Security.Claims;

namespace Stack.Core.Framework
{
    public class AnonymousRequestContext : IEntityRequestContext
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


        public AnonymousRequestContext()
        {
            Who = new AnonymousPrincipal();
        }

        public AnonymousRequestContext(IEnumerable<Claim> claims)
        {
            Who = new AnonymousPrincipal(claims);
        }

        public AnonymousRequestContext(string type, string value)
        {
            Who = new AnonymousPrincipal(type, value);
        }

        public AnonymousRequestContext(string value)
        {
            Who = new AnonymousPrincipal(value);
        }

        public AnonymousRequestContext(Claim claim)
        {
            Who = new AnonymousPrincipal(claim);
        }
    }
}
