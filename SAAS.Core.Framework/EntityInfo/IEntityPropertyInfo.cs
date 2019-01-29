using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SAAS.Core.Framework
{
    public interface IEntityPropertyInfo
    {
        [DataMember(Name = "name")]
        string Name { get; set; }

        [DataMember(Name = "isNullable")]
        bool? IsNullable { get; set; }

        [DataMember(Name = "isKey")]
        bool? IsKey { get; set; }

        [DataMember(Name = "isIdentity")]
        bool? IsIdentity { get; set; }

        [DataMember(Name = "forceUpperCase")]
        bool? ForceUpperCase { get; set; }

        [DataMember(Name = "type")]
        string Type { get; set; }
        
        [DataMember(Name = "default")]
        string Default { get; set; }

        [DataMember(Name = "maxLength")]

        int? MaxLength { get; set; }

        [JsonIgnore]
        Delegate Set { get; set; }

        [JsonIgnore]
        Delegate Get { get; set; }

        [JsonIgnore]
        Type PropertyType { get; set; }
    }
}