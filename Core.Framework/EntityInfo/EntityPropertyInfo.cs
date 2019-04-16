using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Stack.Core.Framework
{
    /// <inheritdoc />
    [DataContract(Name = "entityPropertyInfo")]
    public class EntityPropertyInfo : IEntityPropertyInfo
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "isNullable")]
        public bool? IsNullable { get; set; }

        [DataMember(Name = "isKey")]
        public bool? IsKey { get; set; }

        [DataMember(Name = "isIdentity")]
        public bool? IsIdentity { get; set; }

        [DataMember(Name = "forceUpperCase")]
        public bool? ForceUpperCase { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
        
        [DataMember(Name = "default")]
        public string Default { get; set; }

        [DataMember(Name = "maxLength")]
        public int? MaxLength { get; set; }

        [JsonIgnore]
        public Delegate Set { get; set; }

        [JsonIgnore]
        public Delegate Get { get; set; }

        [JsonIgnore]
        public Type PropertyType { get; set; }
    }
}
