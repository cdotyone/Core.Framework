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
using Civic.Core.Framework.Security;
using Civic.Core.Audit;
using Civic.Core.Logging;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Test.Entities;

using EnvironmentEntity = Civic.Framework.WebApi.Test.Entities.Environment;

namespace Civic.Framework.WebApi.Test.Services
{
    
    public partial class ExampleService
    	{
    	public EnvironmentEntity GetEnvironment( Int32 id) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetEnvironmentByID")) {
    
                if (!AuthorizationHelper.CanView("dbo", "environment")) throw new NotImplementedException();
    
    			try {		
                    using (var database = Connection) {
    					return Data.ExampleData.GetEnvironment(id, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public List<EnvironmentEntity> GetPagedEnvironment(int skip, ref int count, bool retCount, string filterBy, string orderBy) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetPagedEnvironment")) {
    
                if (!AuthorizationHelper.CanView("dbo", "environment")) throw new NotImplementedException();
    
    			try {
                    using (var database = Connection) {
    					return Data.ExampleData.GetPagedEnvironment(skip, ref count, retCount, filterBy, orderBy, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public void AddEnvironment(EnvironmentEntity environment) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "AddEnvironment")) {
    
                if (!AuthorizationHelper.CanAdd("dbo", "environment")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    	                var logid = AuditManager.LogAdd(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", environment.ID.ToString()+"", environment);
    			 		Data.ExampleData.AddEnvironment(environment, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			} 
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void ModifyEnvironment(EnvironmentEntity environment) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "ModifyEnvironment")) {
    
                if (!AuthorizationHelper.CanModify("dbo", "environment")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    					var before = Data.ExampleData.GetEnvironment( environment.ID, db);
    					var logid = AuditManager.LogModify(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.ID.ToString()+"", before, environment);
    					Data.ExampleData.ModifyEnvironment(environment, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void RemoveEnvironment( Int32 id ) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "RemoveEnvironment")) {
    
                if (!AuthorizationHelper.CanRemove("dbo", "environment")) throw new NotImplementedException();
    
    			try {
    				using(var db = Connection) {
    					var before = Data.ExampleData.GetEnvironment( id, db);
    					var logid = AuditManager.LogRemove(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.ID.ToString()+"", before);
    					Data.ExampleData.RemoveEnvironment( id, db);
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

