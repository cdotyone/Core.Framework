using System;
using System.Security.Claims;

namespace Civic.Framework.WebApi
{
    public class EntityRequestContext : IEntityRequestContext
    {
        public ClaimsPrincipal Who { get; set; }

        public string TransactionUID { get; set; } = string.Empty.InsureUID();
    }
}
