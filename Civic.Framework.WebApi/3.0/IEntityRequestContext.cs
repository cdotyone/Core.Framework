using System.Security.Claims;

namespace Civic.Framework.WebApi
{
    public interface IEntityRequestContext
    {
        ClaimsPrincipal Who { get; set; }

        string TransactionUID { get; set; }
    }
}
