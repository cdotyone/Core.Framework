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
using Civic.Core.Data;
using Civic.Framework.WebApi;
using Newtonsoft.Json;
using Civic.Framework.WebApi.Test.Interfaces;

namespace Civic.Framework.WebApi.Test.Entities
{
    [DataContract(Name="installationEnvironment")]
    public partial class InstallationEnvironment : IEntityIdentity
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
        public System.DateTime Modified { get; set; }
    
        [DataMember(Name = "_key")]
    	public string _key 
        { 
    		get {
    			return EnvironmentCode.ToString();
    		}
    	}
    
        [DataMember(Name = "_module")]
        public string _module { get { return Info.Module; } }
        
        [DataMember(Name = "_entity")]
        public string _entity { get { return Info.Entity; } }
        
        public static IEntityInfo Info = new EntityInfo
    	{
            Module = "dbo",
            Entity = "InstallationEnvironment",
            Name = "dbo.InstallationEnvironment",
            Properties = new Dictionary<string, IEntityPropertyInfo>
            {
    			{"environmentCode", new EntityPropertyInfo { Name = "environmentCode", Type="string", IsKey=true }},
    			{"name", new EntityPropertyInfo { Name = "name", Type="string" }},
    			{"description", new EntityPropertyInfo { Name = "description", Type="string", IsNullable=true }},
    			{"isVisible", new EntityPropertyInfo { Name = "isVisible", Type="string" }},
    			{"modified", new EntityPropertyInfo { Name = "modified", Type="DateTime" }},
            }
        };
    
    	private IFacadeInstallationEnvironment _facade;
    	public InstallationEnvironment(IFacadeInstallationEnvironment facade)
    	{
    		_facade = facade;
    	}
    
    	public IEntityInfo GetInfo() {
    		return Info;
    	}
    
        public IEntityIdentity LoadByKey(IEntityRequestContext context, string key) {
    		var parts = key.Split('|');
    			
    		EnvironmentCode = parts[0];
    
    		return Load(context);
    	}
    
        public void RemoveByKey(IEntityRequestContext context, string key) {
    		LoadByKey(context, key).Remove(context);
    	}
    
    	public IEnumerable<IEntityIdentity> GetPaged(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy) {
    		return _facade.GetPagedInstallationEnvironment(context, skip, ref count, retCount, filterBy, orderBy);
    	}
    
    	public IEntityIdentity Load(IEntityRequestContext context) {
    		return _facade.GetInstallationEnvironment(context,EnvironmentCode);
    	}
    
    	public void Save(IEntityRequestContext context) {
    		_facade.SaveInstallationEnvironment(context, this);
    	}
    
    	public void Remove(IEntityRequestContext context) {
    		_facade.RemoveInstallationEnvironment(context,EnvironmentCode);
    	}
    }
}


