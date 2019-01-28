﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is to supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SimpleInjector;
using SAAS.Core.Framework;
using SAAS.Core.Framework.Test.Interfaces;

namespace SAAS.Core.Framework.Test.Entities
{
    [DataContract(Name = "entity2")]
	[Module(Name = "example")]
	public partial class Entity2 : IEntityIdentity
	{

		[DataMember(Name="someID")]
		public int SomeID { get; set; }

		[DataMember(Name="ff")]
		public string ff { get; set; }

		[DataMember(Name="modified")]
		public DateTime Modified { get; set; }

		[DataMember(Name="otherDate")]
		public DateTime? OtherDate { get; set; }

		[DataMember(Name="ouid")]
		public string OUID { get; set; }

		[DataMember(Name = "_key")]
		public string _key 
		{ 
			get {
				return SomeID.ToString()+"|"+ff.ToString();
			}
			set {
				var keys = value.Split('|');
						
				SomeID = Int32.Parse(keys[0]);		
				ff = keys[1];
			}
		}

		[DataMember(Name = "_module")]
		public string _module { get { return "example"; } }
    
		[DataMember(Name = "_entity")]
		public string _entity { get { return "entity2"; } }

		[JsonIgnore]
		public Dictionary<string,object> _extra { get; set; }
	}
}

