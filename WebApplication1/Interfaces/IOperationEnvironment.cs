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
using System.Security.Claims;
using Civic.Framework.WebApi.Test.Entities;

using EnvironmentEntity = Civic.Framework.WebApi.Test.Entities.Environment;

namespace Civic.Framework.WebApi.Test.Interfaces
{
    
    public interface IOperationEnvironment
    {
    	List<EnvironmentEntity> GetPagedEnvironment(ClaimsPrincipal who, int skip, ref int count, bool retCount, string filterBy, string orderBy);
    
    	EnvironmentEntity GetEnvironment(ClaimsPrincipal who,  Int32 id );
    
    	void AddEnvironment(ClaimsPrincipal who, EnvironmentEntity environment);
    
    	void ModifyEnvironment(ClaimsPrincipal who, EnvironmentEntity environment);
    
    	void RemoveEnvironment(ClaimsPrincipal who,  Int32 id );
    }
}

