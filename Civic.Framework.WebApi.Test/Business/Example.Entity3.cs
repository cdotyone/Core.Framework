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

using Entity3Entity = Civic.Framework.WebApi.Test.Entities.Entity3;

namespace Civic.Framework.WebApi.Test.Business
{
    
    public partial class ExampleBusinessFacade
    {
    
    	public Entity3Entity GetEntity3(ClaimsPrincipal who, String someUID) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "GetEntity3")) {
    
                if (!AuthorizationHelper.CanView(who, Entity3Entity.Info)) throw new UnauthorizedAccessException();
    
    			try {		
    				return _respository.GetEntity3(who,  someUID);
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public List<Entity3Entity> GetPagedEntity3(ClaimsPrincipal who, int skip, ref int count, bool retCount, string filterBy, string orderBy) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "GetPagedEntity3")) {
    
                if (!AuthorizationHelper.CanView(who, Entity3Entity.Info)) throw new UnauthorizedAccessException();
    
    			try {
    				return _respository.GetPagedEntity3(who, skip, ref count, retCount, filterBy, orderBy);
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    
    	public void SaveEntity3(ClaimsPrincipal who, Entity3Entity entity) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "SaveEntity3")) {
        
                if (!AuthorizationHelper.CanModify(who, Entity3Entity.Info) && !AuthorizationHelper.CanAdd(who, Entity3Entity.Info)) throw new UnauthorizedAccessException();
        
        		try {
        			var before = _respository.GetEntity3(who,  entity.SomeUID);
    
    			    if (before == null)
    			    {
                        if(!AuthorizationHelper.CanAdd(who, Entity3Entity.Info)) throw new UnauthorizedAccessException();
    
    			        var logid = AuditManager.LogAdd(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", entity.IdentityID, entity);
                        _respository.AddEntity3(who, entity);
    			        AuditManager.MarkSuccessFul("dbo", logid);
                    }
    			    else
    			    {
    			        if (!AuthorizationHelper.CanModify(who, Entity3Entity.Info)) throw new UnauthorizedAccessException();
    
    			        var logid = AuditManager.LogModify(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.IdentityID , before, entity);
    			        _respository.ModifyEntity3(who, entity);
    			        AuditManager.MarkSuccessFul("dbo", logid);
                    }
        		}
        		catch (Exception ex)
        		{
        			if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
        		}
        	}
    	}
    
    	public void RemoveEntity3(ClaimsPrincipal who,  String someUID ) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "RemoveEntity3")) {
    
                if (!AuthorizationHelper.CanRemove(who, Entity3Entity.Info)) throw new UnauthorizedAccessException();
    
    			try {
    				var before = _respository.GetEntity3(who,  someUID);
    				var logid = AuditManager.LogRemove(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.IdentityID , before);
    				_respository.RemoveEntity3(who,  someUID);
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

