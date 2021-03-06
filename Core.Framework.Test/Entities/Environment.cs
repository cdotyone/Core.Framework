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
using Core.Framework;

namespace Core.Framework.Test.Entities
{
    [DataContract(Name = "environment")]
	[Module(Name = "example")]
	public partial class Environment : IEntityIdentity
	{

		[DataMember(Name="id")]
		[PrimaryKey]
		public int ID { get; set; }

		[DataMember(Name="name")]
		public string Name { get; set; }

		[DataMember(Name = "_key")]
		public string _key 
		{ 
			get {
				return ID.ToString();
			}
			set {
				var keys = value.Split('|');
						
				ID = Int32.Parse(keys[0]);
			}
		}

		[DataMember(Name = "_module")]
		public string _module { get { return "example"; } }
    
		[DataMember(Name = "_entity")]
		public string _entity { get { return "environment"; } }

		[JsonIgnore]
		public Dictionary<string,object> _extra { get; set; }
	}
}

