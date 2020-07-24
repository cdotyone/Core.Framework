using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Core.Framework
{
    /// <inheritdoc />
    [DataContract(Name = "entityInfo")]
    public class EntityInfo : IEntityInfo
    {
        [DataMember(Name = "module")]
        public string Module { get; set; }

        [DataMember(Name = "entity")]
        public string Entity { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "relatedModule")]
        public string RelatedModule { get; set; }

        [DataMember(Name = "relatedEntity")]
        public string RelatedEntity { get; set; }

        [DataMember(Name = "relatedKeyName")]
        public string RelatedKeyName { get; set; }


        [DataMember(Name = "forceUpperCase")]
        public bool? ForceUpperCase { get; set; }

        [DataMember(Name = "max")]
        public int? Max { get; set; }

        [DataMember(Name = "canView")]
        public bool? CanView { get; set; }

        [DataMember(Name = "canAdd")]
        public bool? CanAdd { get; set; }

        [DataMember(Name = "canModify")]
        public bool? CanModify { get; set; }

        [DataMember(Name = "canRemove")]
        public bool? CanRemove { get; set; }

        [DataMember(Name = "useLocalTime")]
        public bool? UseLocalTime { get; set; }

        [DataMember(Name = "properties")]
        public Dictionary<string, IEntityPropertyInfo> Properties { get; set; }

        [JsonIgnore]
        public bool Mapped { get; set; }
    }
}
