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
    
    public interface IFacadeEntity2
    {
    	List<Entity2Entity> GetPagedEntity2(ClaimsPrincipal who, int skip, ref int count, bool retCount, string filterBy, string orderBy);
    
    	Entity2Entity GetEntity2(ClaimsPrincipal who,  Int32 someID, String ff );
    
    	void SaveEntity2(ClaimsPrincipal who, Entity2Entity entity2);
    
    	void RemoveEntity2(ClaimsPrincipal who,  Int32 someID, String ff );
    }
}

