using System;
using System.Collections.Generic;
using System.Security.Claims;
using Civic.Core.Configuration;
using Civic.Core.Data;

namespace Civic.Framework.WebApi
{
    [Obsolete("Generation 3 now available")]
    public interface IEntityService
    {
        IDBConnection Connection { get; set; }

        ClaimsPrincipal Who { get; set; }

        INamedElement Configuration { get; set; }

        string ModuleName { get; }

        List<string> EntitiesProvided { get; }

        IEntity Create(string name);
    }
}
