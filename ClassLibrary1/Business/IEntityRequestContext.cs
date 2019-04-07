using System.Collections.Generic;
using System.Security.Claims;

namespace Core.Framework
{
    public interface IEntityRequestContext
    {
        ClaimsPrincipal Who { get; set; }

        string TransactionUID { get; set; }

        EntityEventType IgnoreHandlers { get; set; }

        string Expand { get; set; }

        List<IEntityOperation> Operations { get; }

        void Commit();

        void Rollback();
    }
}
