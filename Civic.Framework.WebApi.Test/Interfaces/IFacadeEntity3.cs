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

using Entity3Entity = Civic.Framework.WebApi.Test.Entities.Entity3;

namespace Civic.Framework.WebApi.Test.Interfaces
{
    
    public interface IFacadeEntity3
    {
    	List<Entity3Entity> GetPagedEntity3(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy);
    
    	Entity3Entity GetEntity3(IEntityRequestContext context,  String someUID );
    
    	void SaveEntity3(IEntityRequestContext context, Entity3Entity entity3);
    
    	void RemoveEntity3(IEntityRequestContext context,  String someUID );
    
    	List<Entity3Entity> GetPagedEntity3(ClaimsPrincipal who, int skip, ref int count, bool retCount, string filterBy, string orderBy);
    
    	Entity3Entity GetEntity3(ClaimsPrincipal who,  String someUID );
    
    	void SaveEntity3(ClaimsPrincipal who, Entity3Entity entity3);
    
    	void RemoveEntity3(ClaimsPrincipal who,  String someUID );
    
    }
}

