using System.Collections.Generic;

namespace Civic.Framework.WebApi
{
    public interface IEntityInfo
    {
        string Module { get; set; }

        string Entity { get; set; }

        string Name { get; set; }

        bool UseProcedureGet { get; set; }

        bool UseProcedureGetPaged { get; set; }

        Dictionary<string, IEntityPropertyInfo> Properties { get; set; }
    }
}
