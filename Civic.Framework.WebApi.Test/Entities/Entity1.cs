﻿
//------------------------------------------------------------------------------
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


using IExampleEntity1 = Civic.Framework.WebApi.Test.Interfaces.IEntity1;

namespace Civic.Framework.WebApi.Test.Entities
{


[DataContract(Name="entity1")]
public partial class Entity1 : IExampleEntity1
{


	[DataMember(Name="name")]
    public string Name { get; set; }


	[DataMember(Name="environmentID")]
    public int EnvironmentID { get; set; }


	[DataMember(Name="dte")]
    public DateTime Dte { get; set; }


	[DataMember(Name="dte2")]
    public DateTime? Dte2 { get; set; }


	[DataMember(Name="dble1")]
    public double Dble1 { get; set; }


	[DataMember(Name="dec1")]
    public double Dec1 { get; set; }


    [DataMember(Name = "_key")]
	public string _key 
    { 
		get {

			return Name.ToString();
		}
		set {
			var keys = value.Split('|');
		
			Name = keys[0];

		}
	}

    [DataMember(Name = "_module")]
    public string _module { get { return Info.Module; } }
    
    [DataMember(Name = "_entity")]
    public string _entity { get { return Info.Entity; } }
    
    public static IEntityInfo Info = new EntityInfo
	{
        Module = "example",
        Entity = "entity1",
        Name = "example.entity1",
        Properties = new Dictionary<string, IEntityPropertyInfo>
        {
			{"name", new EntityPropertyInfo { Name = "name", Type="string", IsKey=true }},
			{"environmentID", new EntityPropertyInfo { Name = "environmentID", Type="int" }},
			{"dte", new EntityPropertyInfo { Name = "dte", Type="DateTime" }},
			{"dte2", new EntityPropertyInfo { Name = "dte2", Type="DateTime>", IsNullable=true }},
			{"dble1", new EntityPropertyInfo { Name = "dble1", Type="double" }},
			{"dec1", new EntityPropertyInfo { Name = "dec1", Type="double" }},


        }
    };

	private readonly Container _container;
	public Entity1(Container container)
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
	    var facade = _container.GetInstance<IEntity1Facade>();
		return facade.GetPaged(context, skip, ref count, retCount, filterBy, orderBy);
	}

	public IEntityIdentity Load(IEntityRequestContext context) {
	    var facade = _container.GetInstance<IEntity1Facade>();
		return facade.Get(context, this);
	}

	public void Save(IEntityRequestContext context) {
	    var facade = _container.GetInstance<IEntity1Facade>();
		facade.Save(context, this);
	}

	public void Remove(IEntityRequestContext context) {
	    var facade = _container.GetInstance<IEntity1Facade>();
		facade.Remove(context, this);
	}
}

}

