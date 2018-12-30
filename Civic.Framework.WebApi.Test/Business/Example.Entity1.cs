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

using Entity1Entity = Civic.Framework.WebApi.Test.Entities.Entity1;

namespace Civic.Framework.WebApi.Test.Business
{
    
    public partial class ExampleBusinessFacade
    {
    
    	public Entity1Entity GetEntity1(IEntityRequestContext context,  String name) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "GetEntity1")) {
    
    			try {	
    				if (!_handlers.OnGetBefore(context, Entity1Entity.Info))
    					return null;
    
    				var entity = _respository.GetEntity1(context,  name);
    				
    				if (!_handlers.OnGetAfter(context, Entity1Entity.Info, entity))
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
    	
    	public List<Entity1Entity> GetPagedEntity1(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "GetPagedEntity1")) {
    
    			try {
    				if (!_handlers.OnGetPagedBefore(context, Entity1Entity.Info))
    					return null;
    
    				var list = _respository.GetPagedEntity1(context, skip, ref count, retCount, filterBy, orderBy);
    
    				list = _handlers.OnGetPagedAfter<Entity1Entity>(context, Entity1Entity.Info, list);
    
    				return list;
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    
    	public void SaveEntity1(IEntityRequestContext context, Entity1Entity entity) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "SaveEntity1")) {
        		try {
        			var before = _respository.GetEntity1(context,  entity.Name);
    
    			    if (before == null)
    			    {
    					if (!_handlers.OnAddBefore(context, Entity1Entity.Info, entity))
    						return;
    
                        _respository.AddEntity1(context, entity);
    
    					if (!_handlers.OnAddAfter(context, Entity1Entity.Info, entity))
    						return;
                    }
    			    else
    			    {
    					if (!_handlers.OnModifyBefore(context, Entity1Entity.Info, before, entity))
    						return;
    
    			        _respository.ModifyEntity1(context, entity);
    
    					if (!_handlers.OnModifyAfter(context, Entity1Entity.Info, before, entity))
    						return;
                    }
        		}
        		catch (Exception ex)
        		{
        			if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
        		}
        	}
    	}
    
    	public void RemoveEntity1(IEntityRequestContext context,  String name ) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleBusinessFacade), "RemoveEntity1")) {
    
    			try {
    				var before = _respository.GetEntity1(context,  name);
    
    				if (!_handlers.OnRemoveBefore(context, Entity1Entity.Info, before))
    					return;
    
    				_respository.RemoveEntity1(context,  name);
    
    				if (!_handlers.OnRemoveAfter(context, Entity1Entity.Info, before))
    					return;
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public List<Entity1Entity> GetPagedEntity1(ClaimsPrincipal who, int skip, ref int count, bool retCount, string filterBy, string orderBy) {
    		return GetPagedEntity1(new EntityRequestContext {Who = who}, skip, ref count, retCount, filterBy, orderBy);
    	}
    
    	public Entity1Entity GetEntity1(ClaimsPrincipal who, String name ) {
    		return GetEntity1(new EntityRequestContext {Who = who}, name);
    	}
    
    	public void SaveEntity1(ClaimsPrincipal who, Entity1Entity entity) {
    		SaveEntity1(new EntityRequestContext {Who = who}, entity);
    	}
    
    	public void RemoveEntity1(ClaimsPrincipal who, String name ) {
    		RemoveEntity1(new EntityRequestContext {Who = who}, name);
    	}
    
    }
}

