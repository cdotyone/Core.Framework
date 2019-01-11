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
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Test.Interfaces;

using IExampleEntity3 = Civic.Framework.WebApi.Test.Interfaces.IEntity3;
namespace Civic.Framework.WebApi.Test.Entities
{
    
    [DataContract(Name="entity3")]
    public partial class Entity3 : IExampleEntity3
    {
    
    	[DataMember(Name="someUID")]
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
        public string _module { get { return Info.Module; } }
        
        [DataMember(Name = "_entity")]
        public string _entity { get { return Info.Entity; } }
        
        public static IEntityInfo Info = new EntityInfo
    	{
            Module = "example",
            Entity = "entity3",
            Name = "example.entity3",
            Properties = new Dictionary<string, IEntityPropertyInfo>
            {
    			{"someUID", new EntityPropertyInfo { Name = "someUID", Type="string", IsKey=true }},
    			{"someID", new EntityPropertyInfo { Name = "someID", Type="long" }},
    			{"modified", new EntityPropertyInfo { Name = "modified", Type="DateTime" }},
    			{"otherDate", new EntityPropertyInfo { Name = "otherDate", Type="DateTime", IsNullable=true }},
    
            }
        };
    
    	private readonly Container _container;
    	public Entity3(Container container)
    	{
    	    _container = container;
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
    	    var facade = _container.GetInstance<IEntity3Facade>();
    		return facade.GetPaged(context, skip, ref count, retCount, filterBy, orderBy);
    	}
    
    	public IEntityIdentity Load(IEntityRequestContext context) {
    	    var facade = _container.GetInstance<IEntity3Facade>();
    		return facade.Get(context, this);
    	}
    
    	public void Save(IEntityRequestContext context) {
    	    var facade = _container.GetInstance<IEntity3Facade>();
    		facade.Save(context, this);
    	}
    
    	public void Remove(IEntityRequestContext context) {
    	    var facade = _container.GetInstance<IEntity3Facade>();
    		facade.Remove(context, this);
    	}
    }
}

