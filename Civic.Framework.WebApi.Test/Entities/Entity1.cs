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
    [DataContract(Name="entity1")]
    public partial class Entity1 : IEntity
    {
    	[DataMember(Name="name")]
        public string Name { get; set; }
    
    	[DataMember(Name="environmentID")]
        public int EnvironmentID { get; set; }
    
    	[DataMember(Name="dte")]
        public System.DateTime Dte { get; set; }
    
    	[DataMember(Name="dte2")]
        public Nullable<System.DateTime> Dte2 { get; set; }
    
    	[DataMember(Name="dble1")]
        public double Dble1 { get; set; }
    
    	[DataMember(Name="dec1")]
        public double Dec1 { get; set; }
    
    
        public Entity1 Copy()
        {
            var copy = new Entity1
                {
    			Name = Name
    			,EnvironmentID = EnvironmentID
    			,Dte = Dte
    			,Dte2 = Dte2
    			,Dble1 = Dble1
    			,Dec1 = Dec1
                };
            return copy;
        }
    
    	public string IdentityID 
        { 
    		get {
    					return null;
    		}
    	}
    
        public void Add(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.AddEntity1(this);
        }
    
        public void Modify(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.ModifyEntity1(this);
        }
    
        public void Remove(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.RemoveEntity1(Name );
        }
    
    }
}

