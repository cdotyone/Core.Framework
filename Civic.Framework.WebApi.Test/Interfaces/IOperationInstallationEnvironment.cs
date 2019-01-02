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

using InstallationEnvironmentEntity = Civic.Framework.WebApi.Test.Entities.InstallationEnvironment;

namespace Civic.Framework.WebApi.Test.Interfaces
{
    
    public interface IOperationInstallationEnvironment
    {
    	List<InstallationEnvironmentEntity> GetPagedInstallationEnvironment(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy );
    
    	InstallationEnvironmentEntity GetInstallationEnvironment(IEntityRequestContext context,  String environmentCode );
    
    	void AddInstallationEnvironment(IEntityRequestContext context, InstallationEnvironmentEntity entity );
    
    	void ModifyInstallationEnvironment(IEntityRequestContext context, InstallationEnvironmentEntity before, InstallationEnvironmentEntity after );
    
    	void RemoveInstallationEnvironment(IEntityRequestContext context, InstallationEnvironmentEntity entity );
    }
}

