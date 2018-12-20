using System.Security.Claims;
using Civic.Core.Data;

namespace Civic.Framework.WebApi
{
    public interface IDataConnectionProvider
    {
        IDBConnection Connection { get; }

        ClaimsPrincipal Who { get; }
    }
}
