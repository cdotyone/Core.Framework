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

namespace Civic.Framework.WebApi.Test.Entities
{
    [DataContract(Name="environment")]
    public partial class Environment : IEntity
    {
    	[DataMember(Name="id")]
        public int ID { get; set; }
    
    	[DataMember(Name="name")]
        public string Name { get; set; }
    
    
        public Environment Copy()
        {
            var copy = new Environment
                {
    			ID = ID
    			,Name = Name
                };
            return copy;
        }
    
    	public string IdentityID 
        { 
    		get {
    	return this.ID.ToString();
    }
    	}
    
        public void Add(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.AddEnvironment(this);
        }
    
        public void Modify(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.ModifyEnvironment(this);
        }
    
        public void Remove(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.RemoveEnvironment(ID );
        }
    
    }
}


