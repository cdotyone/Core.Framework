using System.Collections.Generic;

namespace SAAS.Core.Framework
{
    /// <inheritdoc />
    public class EntityInfo : IEntityInfo
    {
        public string Module { get; set; }

        public string Entity { get; set; }

        public string Name { get; set; }

        public bool UseProcedureGet { get; set; }

        public bool UseProcedureGetPaged { get; set; }

        public string RelatedModule { get; set; }

        public string RelatedEntity { get; set; }

        public string RelatedKeyName { get; set; }

        public Dictionary<string, IEntityPropertyInfo> Properties { get; set; }

        public bool Mapped { get; set; }
    }
}
