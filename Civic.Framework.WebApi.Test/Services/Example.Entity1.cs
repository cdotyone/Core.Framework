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
using System.ServiceModel.Activation;
using System.Collections.Generic;
using Civic.Core.Security;
using Civic.Core.Audit;
using Civic.Core.Logging;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Test.Entities;

using Entity1Entity = Civic.Framework.WebApi.Test.Entities.Entity1;

namespace Civic.Framework.WebApi.Test.Services
{
    
    public partial class ExampleService
    	{
    	public Entity1Entity GetEntity1( String name) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetEntity1ByName")) {
    
                if (!AuthorizationHelper.CanView("dbo", "entity1")) throw new NotImplementedException();
    
    			try {		
                    using (var database = Connection) {
    					return Data.ExampleData.GetEntity1(name, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public List<Entity1Entity> GetPagedEntity1(int skip, ref int count, bool retCount, string filterBy, string orderBy) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetPagedEntity1")) {
    
                if (!AuthorizationHelper.CanView("dbo", "entity1")) throw new NotImplementedException();
    
    			try {
                    using (var database = Connection) {
    					return Data.ExampleData.GetPagedEntity1(skip, ref count, retCount, filterBy, orderBy, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public void AddEntity1(Entity1Entity entity1) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "AddEntity1")) {
    
                if (!AuthorizationHelper.CanAdd("dbo", "entity1")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    	                var logid = AuditManager.LogAdd(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", entity1.Name.ToString()+"", entity1);
    			 		Data.ExampleData.AddEntity1(entity1, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    
    				}
    			} 
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void ModifyEntity1(Entity1Entity entity1) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "ModifyEntity1")) {
    
                if (!AuthorizationHelper.CanModify("dbo", "entity1")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    					var before = Data.ExampleData.GetEntity1( entity1.Name, db);
    					var logid = AuditManager.LogModify(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.Name.ToString()+"", before, entity1);
    					Data.ExampleData.ModifyEntity1(entity1, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void RemoveEntity1( String name ) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "RemoveEntity1")) {
    
                if (!AuthorizationHelper.CanRemove("dbo", "entity1")) throw new NotImplementedException();
    
    			try {
    				using(var db = Connection) {
    					var before = Data.ExampleData.GetEntity1( name, db);
    					var logid = AuditManager.LogRemove(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.Name.ToString()+"", before);
    					Data.ExampleData.RemoveEntity1( name, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    }
}

