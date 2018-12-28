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
using System.Security.Claims;
using System.Collections.Generic;
using Civic.Core.Security;
using Civic.Core.Audit;
using Civic.Core.Logging;

using EnvironmentEntity = Civic.Framework.WebApi.Test.Entities.Environment;

namespace Civic.Framework.WebApi.Test.Business
{
    
    public partial class ExampleBusinessFacade
    {
    
    	public EnvironmentEntity GetEnvironment(ClaimsPrincipal who, Int32 id) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "GetEnvironment")) {
    
                if (!AuthorizationHelper.CanView(who, EnvironmentEntity.Info)) throw new UnauthorizedAccessException();
    
    			try {		
    				return _respository.GetEnvironment(who,  id);
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public List<EnvironmentEntity> GetPagedEnvironment(ClaimsPrincipal who, int skip, ref int count, bool retCount, string filterBy, string orderBy) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "GetPagedEnvironment")) {
    
                if (!AuthorizationHelper.CanView(who, EnvironmentEntity.Info)) throw new UnauthorizedAccessException();
    
    			try {
    				return _respository.GetPagedEnvironment(who, skip, ref count, retCount, filterBy, orderBy);
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    
    	public void SaveEnvironment(ClaimsPrincipal who, EnvironmentEntity entity) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "SaveEnvironment")) {
        
                if (!AuthorizationHelper.CanModify(who, EnvironmentEntity.Info) && !AuthorizationHelper.CanAdd(who, EnvironmentEntity.Info)) throw new UnauthorizedAccessException();
        
        		try {
        			var before = _respository.GetEnvironment(who,  entity.ID);
    
    			    if (before == null)
    			    {
                        if(!AuthorizationHelper.CanAdd(who, EnvironmentEntity.Info)) throw new UnauthorizedAccessException();
    
    			        var logid = AuditManager.LogAdd(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", entity.IdentityID, entity);
                        _respository.AddEnvironment(who, entity);
    			        AuditManager.MarkSuccessFul("dbo", logid);
                    }
    			    else
    			    {
    			        if (!AuthorizationHelper.CanModify(who, EnvironmentEntity.Info)) throw new UnauthorizedAccessException();
    
    			        var logid = AuditManager.LogModify(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.IdentityID , before, entity);
    			        _respository.ModifyEnvironment(who, entity);
    			        AuditManager.MarkSuccessFul("dbo", logid);
                    }
        		}
        		catch (Exception ex)
        		{
        			if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
        		}
        	}
    	}
    
    	public void RemoveEnvironment(ClaimsPrincipal who,  Int32 id ) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "RemoveEnvironment")) {
    
                if (!AuthorizationHelper.CanRemove(who, EnvironmentEntity.Info)) throw new UnauthorizedAccessException();
    
    			try {
    				var before = _respository.GetEnvironment(who,  id);
    				var logid = AuditManager.LogRemove(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.IdentityID , before);
    				_respository.RemoveEnvironment(who,  id);
    				AuditManager.MarkSuccessFul("dbo", logid);
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    }
}

