using System.Collections.Generic;

namespace SAAS.Core.Framework
{
    public interface IEntityInfo
    {
        string Module { get; set; }

        string Entity { get; set; }

        string Name { get; set; }

        bool UseProcedureGet { get; set; }

        bool UseProcedureGetPaged { get; set; }

        string RelatedModule { get; set; }

        string RelatedEntity { get; set; }

        string RelatedKeyName { get; set; }

        Dictionary<string, IEntityPropertyInfo> Properties { get; set; }
    }
}
