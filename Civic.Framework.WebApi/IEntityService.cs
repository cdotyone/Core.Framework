﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civic.Core.Configuration;
using Civic.Core.Data;

namespace Civic.Framework.WebApi
{
    public interface IEntityService
    {
        IDBConnection Connection { get; set; }

        INamedElement Configuration { get; set; }

        string ModuleName { get; }

        List<string> EntitiesProvided { get; }

        IEntity Create(string name);
    }
}