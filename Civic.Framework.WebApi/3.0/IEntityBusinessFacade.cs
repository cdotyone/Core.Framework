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
    public interface IEntityBusinessFacade
    {
        string ModuleName { get; }

        List<string> EntitiesProvided { get; }

        IEntityIdentity Create(string schema, string name);
    }
}
