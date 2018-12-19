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

using Entity3Entity = Civic.Framework.WebApi.Test.Entities.Entity3;

namespace Civic.Framework.WebApi.Test.Services
{
    
    public partial class ExampleService
    	{
    	public Entity3Entity GetEntity3( String someUID) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetEntity3BySomeUID")) {
    
                if (!AuthorizationHelper.CanView("dbo", "entity3")) throw new NotImplementedException();
    
    			try {		
                    using (var database = Connection) {
    					return Data.ExampleData.GetEntity3(someUID, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public List<Entity3Entity> GetPagedEntity3(int skip, ref int count, bool retCount, string filterBy, string orderBy) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetPagedEntity3")) {
    
                if (!AuthorizationHelper.CanView("dbo", "entity3")) throw new NotImplementedException();
    
    			try {
                    using (var database = Connection) {
    					return Data.ExampleData.GetPagedEntity3(skip, ref count, retCount, filterBy, orderBy, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public void AddEntity3(Entity3Entity entity3) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "AddEntity3")) {
    
                if (!AuthorizationHelper.CanAdd("dbo", "entity3")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    	                var logid = AuditManager.LogAdd(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", entity3.SomeUID.ToString()+"", entity3);
    			 		Data.ExampleData.AddEntity3(entity3, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    
    				}
    			} 
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void ModifyEntity3(Entity3Entity entity3) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "ModifyEntity3")) {
    
                if (!AuthorizationHelper.CanModify("dbo", "entity3")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    					var before = Data.ExampleData.GetEntity3( entity3.SomeUID, db);
    					var logid = AuditManager.LogModify(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.SomeUID.ToString()+"", before, entity3);
    					Data.ExampleData.ModifyEntity3(entity3, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void RemoveEntity3( String someUID ) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "RemoveEntity3")) {
    
                if (!AuthorizationHelper.CanRemove("dbo", "entity3")) throw new NotImplementedException();
    
    			try {
    				using(var db = Connection) {
    					var before = Data.ExampleData.GetEntity3( someUID, db);
    					var logid = AuditManager.LogRemove(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.SomeUID.ToString()+"", before);
    					Data.ExampleData.RemoveEntity3( someUID, db);
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
