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
using Civic.Core.Logging;

using Entity3Entity = Civic.Framework.WebApi.Test.Entities.Entity3;

namespace Civic.Framework.WebApi.Test.Business
{
    
    public partial class ExampleBusinessFacade
    {
    
    	public Entity3Entity GetEntity3(IEntityRequestContext context,  String someUID) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "GetEntity3")) {
    
    			try {	
    				if (!_handlers.OnGetBefore(context, Entity3Entity.Info))
    					return null;
    
    				var entity = _respository.GetEntity3(context,  someUID);
    				
    				if (!_handlers.OnGetAfter(context, Entity3Entity.Info, entity))
    					return null;
    
    				return entity;
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public List<Entity3Entity> GetPagedEntity3(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "GetPagedEntity3")) {
    
    			try {
    				if (!_handlers.OnGetPagedBefore(context, Entity3Entity.Info))
    					return null;
    
    				var list = _respository.GetPagedEntity3(context, skip, ref count, retCount, filterBy, orderBy);
    
    				list = _handlers.OnGetPagedAfter<Entity3Entity>(context, Entity3Entity.Info, list);
    
    				return list;
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    
    	public void SaveEntity3(IEntityRequestContext context, Entity3Entity entity) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "SaveEntity3")) {
        		try {
        			var before = _respository.GetEntity3(context,  entity.SomeUID);
    
    			    if (before == null)
    			    {
    					if (!_handlers.OnAddBefore(context, Entity3Entity.Info, entity))
    						return;
    
                        _respository.AddEntity3(context, entity);
    
    					if (!_handlers.OnAddAfter(context, Entity3Entity.Info, entity))
    						return;
                    }
    			    else
    			    {
    					if (!_handlers.OnModifyBefore(context, Entity3Entity.Info, before, entity))
    						return;
    
    			        _respository.ModifyEntity3(context, entity);
    
    					if (!_handlers.OnModifyAfter(context, Entity3Entity.Info, before, entity))
    						return;
                    }
        		}
        		catch (Exception ex)
        		{
        			if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
        		}
        	}
    	}
    
    	public void RemoveEntity3(IEntityRequestContext context,  String someUID ) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "RemoveEntity3")) {
    
    			try {
    				var before = _respository.GetEntity3(context,  someUID);
    
    				if (!_handlers.OnRemoveBefore(context, Entity3Entity.Info, before))
    					return;
    
    				_respository.RemoveEntity3(context,  someUID);
    
    				if (!_handlers.OnRemoveAfter(context, Entity3Entity.Info, before))
    					return;
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public List<Entity3Entity> GetPagedEntity3(ClaimsPrincipal who, int skip, ref int count, bool retCount, string filterBy, string orderBy) {
    		return GetPagedEntity3(new EntityRequestContext {Who = who}, skip, ref count, retCount, filterBy, orderBy);
    	}
    
    	public Entity3Entity GetEntity3(ClaimsPrincipal who, String someUID ) {
    		return GetEntity3(new EntityRequestContext {Who = who}, someUID);
    	}
    
    	public void SaveEntity3(ClaimsPrincipal who, Entity3Entity entity) {
    		SaveEntity3(new EntityRequestContext {Who = who}, entity);
    	}
    
    	public void RemoveEntity3(ClaimsPrincipal who, String someUID ) {
    		RemoveEntity3(new EntityRequestContext {Who = who}, someUID);
    	}
    
    }
}

