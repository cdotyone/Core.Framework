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
using System.Security.Claims;
using System.Collections.Generic;
using Civic.Core.Security;
using Civic.Core.Audit;
using Civic.Core.Logging;

using Entity2Entity = Civic.Framework.WebApi.Test.Entities.Entity2;

namespace Civic.Framework.WebApi.Test.Business
{
    
    public partial class ExampleBusinessFacade
    {
    
    	public Entity2Entity GetEntity2(ClaimsPrincipal who, Int32 someID, String ff) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "GetEntity2")) {
    
                if (!AuthorizationHelper.CanView(who, Entity2Entity.Info)) throw new NotImplementedException();
    
    			try {		
    				return _respository.GetEntity2(who,  someID, ff);
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public List<Entity2Entity> GetPagedEntity2(ClaimsPrincipal who, int skip, ref int count, bool retCount, string filterBy, string orderBy) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "GetPagedEntity2")) {
    
                if (!AuthorizationHelper.CanView(who, Entity2Entity.Info)) throw new NotImplementedException();
    
    			try {
    				return _respository.GetPagedEntity2(who, skip, ref count, retCount, filterBy, orderBy);
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    
    	public void SaveEntity2(ClaimsPrincipal who, Entity2Entity entity) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "SaveEntity2")) {
        
                if (!AuthorizationHelper.CanModify(who, entity) && !AuthorizationHelper.CanAdd(who, entity)) throw new UnauthorizedAccessException();
        
        		try {
        			var before = _respository.GetEntity2(who,  entity.SomeID, entity.ff);
    
    			    if (before == null)
    			    {
                        if(!AuthorizationHelper.CanAdd(who, entity)) throw new UnauthorizedAccessException();
    
    			        var logid = AuditManager.LogAdd(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", entity.IdentityID, entity);
                        _respository.AddEntity1(who, entity);
    			        AuditManager.MarkSuccessFul("dbo", logid);
                    }
    			    else
    			    {
    			        if (!AuthorizationHelper.CanModify(who, entity)) throw new UnauthorizedAccessException();
    
    			        var logid = AuditManager.LogModify(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.IdentityID , before, entity);
    			        _respository.ModifyEntity1(who, entity);
    			        AuditManager.MarkSuccessFul("dbo", logid);
                    }
        		}
        		catch (Exception ex)
        		{
        			if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
        		}
        	}
    	}
    
    	public void RemoveEntity2(ClaimsPrincipal who,  Int32 someID, String ff ) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "RemoveEntity2")) {
    
                if (!AuthorizationHelper.CanRemove(who, Entity2Entity.Info)) throw new NotImplementedException();
    
    			try {
    				var before = _respository.GetEntity2(who,  someID, ff);
    				var logid = AuditManager.LogRemove(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.IdentityID , before);
    				_respository.RemoveEntity2(who,  someID, ff);
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

