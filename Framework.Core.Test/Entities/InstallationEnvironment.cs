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

using IExampleInstallationEnvironment = Civic.Framework.WebApi.Test.Interfaces.IInstallationEnvironment;
namespace Civic.Framework.WebApi.Test.Entities
{
    
    [DataContract(Name="installationEnvironment")]
    public partial class InstallationEnvironment : IExampleInstallationEnvironment
    {
    
    	[DataMember(Name="environmentCode")]
    	public string EnvironmentCode { get; set; }
    
    	[DataMember(Name="name")]
    	public string Name { get; set; }
    
    	[DataMember(Name="description")]
    	public string Description { get; set; }
    
    	[DataMember(Name="isVisible")]
    	public string IsVisible { get; set; }
    
    	[DataMember(Name="modified")]
    	public DateTime Modified { get; set; }
    
        [DataMember(Name = "_key")]
    	public string _key 
        { 
    		get {
    			return EnvironmentCode.ToString();
    		}
    		set {
    			var keys = value.Split('|');
    		
    			EnvironmentCode = keys[0];
    		}
    	}
    
        [DataMember(Name = "_module")]
        public string _module { get { return Info.Module; } }
        
        [DataMember(Name = "_entity")]
        public string _entity { get { return Info.Entity; } }
        
        public static IEntityInfo Info = new EntityInfo
    	{
            Module = "example",
            Entity = "installationEnvironment",
            Name = "example.installationEnvironment",
            Properties = new Dictionary<string, IEntityPropertyInfo>
            {
    			{"environmentCode", new EntityPropertyInfo { Name = "environmentCode", Type="string", IsKey=true }},
    			{"name", new EntityPropertyInfo { Name = "name", Type="string" }},
    			{"description", new EntityPropertyInfo { Name = "description", Type="string", IsNullable=true }},
    			{"isVisible", new EntityPropertyInfo { Name = "isVisible", Type="string" }},
    			{"modified", new EntityPropertyInfo { Name = "modified", Type="DateTime" }},
    
            }
        };
    
    	private readonly Container _container;
    	public InstallationEnvironment(Container container)
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
    	    var facade = _container.GetInstance<IInstallationEnvironmentFacade>();
    		return facade.GetPaged(context, skip, ref count, retCount, filterBy, orderBy);
    	}
    
    	public IEntityIdentity Load(IEntityRequestContext context) {
    	    var facade = _container.GetInstance<IInstallationEnvironmentFacade>();
    		return facade.Get(context, this);
    	}
    
    	public void Save(IEntityRequestContext context) {
    	    var facade = _container.GetInstance<IInstallationEnvironmentFacade>();
    		facade.Save(context, this);
    	}
    
    	public void Remove(IEntityRequestContext context) {
    	    var facade = _container.GetInstance<IInstallationEnvironmentFacade>();
    		facade.Remove(context, this);
    	}
    }
}


