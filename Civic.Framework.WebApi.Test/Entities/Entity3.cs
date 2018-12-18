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
    [DataContract(Name="entity3")]
    public partial class Entity3 : IEntity2
    {
    	[DataMember(Name="someUID")]
        public string SomeUID { get; set; }
    
    	[DataMember(Name="someID")]
    	[JsonIgnore]
        public long SomeID { get; set; }
    
    	[DataMember(Name="modified")]
        public System.DateTime Modified { get; set; }
    
    	[DataMember(Name="otherDate")]
        public Nullable<System.DateTime> OtherDate { get; set; }
    
    
        public Entity3 Copy()
        {
            var copy = new Entity3
                {
    			SomeUID = SomeUID,
    			SomeID = SomeID,
    			Modified = Modified,
    			OtherDate = OtherDate
                };
            return copy;
        }
    
    	public string IdentityID 
        { 
    		get {
    			return this.SomeUID.ToString();
    		}
    	}
    
    	#region IEntity
    
        public void Add(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		this.Add(service);
        }
    
        public void Modify(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		this.Modify(service);
        }
    
        public void Remove(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		this.Remove(service);
        }
    
    	#endregion IEntity
    
    	#region IEntity2
    
        public void Add(IEntityService iservice)
        {
            var service = iservice as Services.ExampleService;
            service.AddEntity3(this);
        }
    
        public void Modify(IEntityService iservice)
        {
            var service = iservice as Services.ExampleService;
            service.ModifyEntity3(this);
        }
    
        public void Remove(IEntityService iservice)
        {
            var service = iservice as Services.ExampleService;
            service.RemoveEntity3(SomeUID );
        }
    
    	#endregion IEntity2
    }
}

