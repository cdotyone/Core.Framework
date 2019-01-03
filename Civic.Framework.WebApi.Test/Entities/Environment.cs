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

using IExampleEnvironment = Civic.Framework.WebApi.Test.Interfaces.IEnvironment;
namespace Civic.Framework.WebApi.Test.Entities
{
    
    [DataContract(Name="environment")]
    public partial class Environment : IExampleEnvironment
    {
    
    	[DataMember(Name="id")]
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
        public string _module { get { return Info.Module; } }
        
        [DataMember(Name = "_entity")]
        public string _entity { get { return Info.Entity; } }
        
        public static IEntityInfo Info = new EntityInfo
    	{
            Module = "example",
            Entity = "environment",
            Name = "example.environment",
            Properties = new Dictionary<string, IEntityPropertyInfo>
            {
    			{"id", new EntityPropertyInfo { Name = "id", Type="int", IsKey=true }},
    			{"name", new EntityPropertyInfo { Name = "name", Type="string", IsNullable=true }},
    
            }
        };
    
    	private IEntityBusinessFacade<IExampleEnvironment> _facade;
    	public Environment(IEntityBusinessFacade<IExampleEnvironment> facade)
    	{
    		_facade = facade;
    	}
    
    	public IEntityInfo GetInfo() {
    		return Info;
    	}
    
        public IEntityIdentity LoadByKey(IEntityRequestContext context, string key) {
    		_key = key;
    		return Load(context);
    	}
    
        public void RemoveByKey(IEntityRequestContext context, string key) {
    		_key = key;
    		Remove(context);
    	}
    
    	public IEnumerable<IEntityIdentity> GetPaged(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy) {
    		return _facade.GetPaged(context, Info, skip, ref count, retCount, filterBy, orderBy);
    	}
    
    	public IEntityIdentity Load(IEntityRequestContext context) {
    		return _facade.Get(context, this);
    	}
    
    	public void Save(IEntityRequestContext context) {
    		_facade.Save(context, this);
    	}
    
    	public void Remove(IEntityRequestContext context) {
    		_facade.Remove(context, this);
    	}
    }
}

