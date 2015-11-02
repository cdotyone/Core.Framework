using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civic.Core.Configuration;

namespace Civic.T4.WebApi
{
    public interface IEntityService
    {
        INamedElement Configuration { get; set; }

        string ModuleName { get; }

        List<string> EntitiesProvided { get; }

        IEntity Create(string name);
    }
}
