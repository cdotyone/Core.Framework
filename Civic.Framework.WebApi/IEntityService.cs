using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Civic.Core.Configuration;
using Civic.Core.Data;
using SimpleInjector;

namespace Civic.Framework.WebApi
{
    [Obsolete]
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
