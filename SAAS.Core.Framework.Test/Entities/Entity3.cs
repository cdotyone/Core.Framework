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

namespace SAAS.Core.Framework.Test.Entities
{
    [DataContract(Name = "entity3")]
	[Module(Name = "example")]
	public partial class Entity3 : IEntityIdentity
	{

		[DataMember(Name="someUID")]
		[PrimaryKey]
		public string SomeUID { get; set; }

		[DataMember(Name="modified")]
		public DateTime Modified { get; set; }

		[DataMember(Name="otherDate")]
		public DateTime? OtherDate { get; set; }

		[DataMember(Name = "_key")]
		public string _key 
		{ 
			get {
				return SomeUID.ToString();
			}
			set {
				var keys = value.Split('|');
		
				SomeUID = keys[0];
			}
		}

		[DataMember(Name = "_module")]
		public string _module { get { return "example"; } }
    
		[DataMember(Name = "_entity")]
		public string _entity { get { return "entity3"; } }

		[JsonIgnore]
		public Dictionary<string,object> _extra { get; set; }
	}
}

