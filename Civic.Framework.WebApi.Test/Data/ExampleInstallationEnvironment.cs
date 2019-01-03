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
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using SimpleInjector;
using Civic.Core.Data;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Configuration;
using Civic.Framework.WebApi.Test.Entities;
using Civic.Framework.WebApi.Test.Interfaces;

using IExampleInstallationEnvironment = Civic.Framework.WebApi.Test.Interfaces.IInstallationEnvironment;
namespace Civic.Framework.WebApi.Test.Data.SqlServer
{
    public partial class InstallationEnvironmentRepository : IEntityRepository<IExampleInstallationEnvironment>
    {
        Container _container;
    	private readonly IEntityCreateFactory _factory;
    
        public InstallationEnvironmentRepository(Container container, IEntityCreateFactory factory)
        {
            _container = container;
    		_factory = factory;
        }
    
    	public IExampleInstallationEnvironment Get(IEntityRequestContext context,  IExampleInstallationEnvironment entity)
    	{
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Get, null, null ,context)) {
    
    			Debug.Assert(database!=null);
    
    			var info = entity.GetInfo();
    
    		    if (!info.UseProcedureGet)
    		    {
    		        return SqlQuery.Get(_container, context.Who, entity, database);
    		    }
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_InstallationEnvironmentGet"))
    			{
    				command.AddInParameter("@environmentCode", entity.EnvironmentCode);
    				
        			command.ExecuteReader(dataReader =>
    				{
    				    if(!SqlQuery.PopulateEntity(context, entity, dataReader)) {
    						entity = null;
    					}
       				});
    			}
    			return entity;
    		}
    	}
    
    	public IEnumerable<IExampleInstallationEnvironment> GetPaged(IEntityRequestContext context, IEntityInfo info, int skip, ref int count, bool retCount, string filterBy, string orderBy)
    	{ 
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Get, null, null ,context)) {
    
    			Debug.Assert(database!=null);
    
    			var list = new List<IExampleInstallationEnvironment>();
    
    		    if (!info.UseProcedureGet)
    		    {
    		        var entityList = SqlQuery.GetPaged<IExampleInstallationEnvironment>(_container, context.Who, info, skip, ref count, retCount, filterBy, orderBy, database);
    		        foreach (var entity in entityList)
    		        {
    		            list.Add(entity as IExampleInstallationEnvironment);
    		        }
    
    		        return list;
    		    }
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_InstallationEnvironmentGetFiltered"))
    			{
    				command.AddInParameter("@skip", skip);			
    				command.AddInParameter("@retcount", retCount);
    				if(!string.IsNullOrEmpty(filterBy)) command.AddInParameter("@filterBy", filterBy);
    				command.AddInParameter("@orderBy", orderBy);
        			command.AddParameter("@count", ParameterDirection.InputOutput, count);
    			
    				command.ExecuteReader(dataReader =>
    					{
                            IExampleInstallationEnvironment item = _factory.CreateNew(info) as IExampleInstallationEnvironment;
    						while(SqlQuery.PopulateEntity(context, item, dataReader))
    						{
    							list.Add(item);
    	                        item = _factory.CreateNew(info) as IExampleInstallationEnvironment;
    						} 
    					});
    
    				if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
    			}
    			return list;
    		}
    	}
    
    	public void Add(IEntityRequestContext context, IExampleInstallationEnvironment  entity)
    	{ 
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Add, entity, null ,context)) {
    
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_InstallationEnvironmentAdd"))
    			{
    				buildInstallationEnvironmentCommandParameters(context, entity, command, true );
    				command.ExecuteNonQuery();
    			}
    		}
    	}
    
    	public void Modify(IEntityRequestContext context, IExampleInstallationEnvironment  before, IExampleInstallationEnvironment  after)
    	{ 
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Modify, before, after, context)) {
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_InstallationEnvironmentModify"))
    			{
    				buildInstallationEnvironmentCommandParameters(context, before, command, false );
    				command.ExecuteNonQuery();
    			}
    		}
    	}
    
    	public void Remove(IEntityRequestContext context, IExampleInstallationEnvironment  entity )
    	{
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Remove, entity, null, context)) {
    
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_InstallationEnvironmentRemove"))
    			{
    				buildInstallationEnvironmentCommandParameters(context, entity, command, false );
    				command.ExecuteNonQuery();
    			}
    		}
    	}
    
    	static void buildInstallationEnvironmentCommandParameters(IEntityRequestContext context, IExampleInstallationEnvironment entity, IDBCommand command, bool addRecord )
    	{ 
            Debug.Assert(command!=null);
       		if(addRecord) command.AddParameter("@environmentcode", ParameterDirection.InputOutput,  T4Config.CheckUpperCase("dbo","installationenvironment","environmentcode",entity.EnvironmentCode));
    		else command.AddInParameter("@environmentcode", T4Config.CheckUpperCase("dbo","installationenvironment","environmentcode",entity.EnvironmentCode));
    		command.AddInParameter("@name", T4Config.CheckUpperCase("dbo","installationenvironment","name",entity.Name));
    		command.AddInParameter("@description", T4Config.CheckUpperCase("dbo","installationenvironment","description",entity.Description, false));
    		command.AddInParameter("@isvisible", T4Config.CheckUpperCase("dbo","installationenvironment","isvisible",entity.IsVisible));
    	}
    }
}

