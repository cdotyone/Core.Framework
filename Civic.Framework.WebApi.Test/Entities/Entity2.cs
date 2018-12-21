﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Civic.Core.Data;
using Civic.Framework.WebApi;
using Newtonsoft.Json;

namespace Civic.Framework.WebApi.Test.Entities
{
    [DataContract(Name="entity2")]
    public partial class Entity2 : IEntityIdentity
    {
    
    	[DataMember(Name="someID")]
        public int SomeID { get; set; }
    
    	[DataMember(Name="ff")]
        public string ff { get; set; }
    
    	[DataMember(Name="modified")]
        public System.DateTime Modified { get; set; }
    
    	[DataMember(Name="otherDate")]
        public Nullable<System.DateTime> OtherDate { get; set; }
    
    	[DataMember(Name="oid")]
    	[JsonIgnore]
        public string OID { get; set; }
    
    	[DataMember(Name="OUID")]
    	public string OUID { get; set; }
    
    	public string IdentityID 
        { 
    		get {
    			return SomeID.ToString()+"|"+ff.ToString();
    		}
    	}
    
    
        [DataMember(Name = "_schema")]
        public string schema { get { return "dbo"; } }
    
        [DataMember(Name = "_entity")]
        public string entity { get { return "Entity2"; } }
    
    	public static IEntityIdentity Info = new Entity2();
    }
}

