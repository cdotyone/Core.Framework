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

using Entity2Entity = Civic.Framework.WebApi.Test.Entities.Entity2;

namespace Civic.Framework.WebApi.Test.Interfaces
{
    
    public interface IOperationEntity2
    {
    	List<Entity2Entity> GetPagedEntity2(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy );
    
    	Entity2Entity GetEntity2(IEntityRequestContext context,  Int32 someID, String ff );
    
    	void AddEntity2(IEntityRequestContext context, Entity2Entity entity );
    
    	void ModifyEntity2(IEntityRequestContext context, Entity2Entity before, Entity2Entity after );
    
    	void RemoveEntity2(IEntityRequestContext context, Entity2Entity entity );
    }
}

