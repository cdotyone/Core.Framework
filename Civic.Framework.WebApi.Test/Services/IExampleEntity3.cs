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
using System.ServiceModel;
using Civic.Framework.WebApi.Test.Entities;

using Entity3Entity = Civic.Framework.WebApi.Test.Entities.Entity3;

namespace Civic.Framework.WebApi.Test.Services
{
    
    [ServiceContract(Namespace = "http://example.civic360.com/")]
    public interface IExampleEntity3
    {
    	[OperationContract]
    	List<Entity3Entity> GetPagedEntity3(int skip, ref int count, bool retCount, string filterBy, string orderBy);
    
    	[OperationContract]
    	Entity3Entity GetEntity3( String someUID, String ff);
    
    	[OperationContract]
    	void AddEntity3(Entity3Entity entity3);
    
    	[OperationContract]
    	void ModifyEntity3(Entity3Entity entity3);
    
    	[OperationContract]
    	void RemoveEntity3( String someUID, String ff );
    }
}

