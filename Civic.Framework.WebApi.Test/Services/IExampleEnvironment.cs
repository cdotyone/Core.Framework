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

using EnvironmentEntity = Civic.Framework.WebApi.Test.Entities.Environment;

namespace Civic.Framework.WebApi.Test.Services
{
    
    [ServiceContract(Namespace = "http://example.civic360.com/")]
    public interface IExampleEnvironment
    {
    	[OperationContract]
    	List<EnvironmentEntity> GetPagedEnvironment(int skip, ref int count, bool retCount, string filterBy, string orderBy);
    
    	[OperationContract]
    	EnvironmentEntity GetEnvironment( Int32 id);
    
    	[OperationContract]
    	void AddEnvironment(EnvironmentEntity environment);
    
    	[OperationContract]
    	void ModifyEnvironment(EnvironmentEntity environment);
    
    	[OperationContract]
    	void RemoveEnvironment( Int32 id );
    }
}



