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

using IExampleEnvironment = Civic.Framework.WebApi.Test.Interfaces.IEnvironment;
namespace Civic.Framework.WebApi.Test.Data.SqlServer
{
    public partial class EnvironmentRepository : IEntityRepository<IExampleEnvironment>
    {
        Container _container;
    	private readonly IEntityCreateFactory _factory;
    
        public EnvironmentRepository(Container container, IEntityCreateFactory factory)
        {
            _container = container;
    		_factory = factory;
        }
    
    	public IExampleEnvironment Get(IEntityRequestContext context,  IExampleEnvironment entity)
    	{
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Get, null, null ,context)) {
    
    			Debug.Assert(database!=null);
    
    			var info = entity.GetInfo();
    
    		    if (!info.UseProcedureGet)
    		    {
    		        return SqlQuery.Get(_container, context.Who, entity, database);
    		    }
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentGet"))
    			{
    				command.AddInParameter("@id", entity.ID);
    				
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
    
    	public IEnumerable<IExampleEnvironment> GetPaged(IEntityRequestContext context, IEntityInfo info, int skip, ref int count, bool retCount, string filterBy, string orderBy)
    	{ 
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Get, null, null ,context)) {
    
    			Debug.Assert(database!=null);
    
    			var list = new List<IExampleEnvironment>();
    
    		    if (!info.UseProcedureGet)
    		    {
    		        var entityList = SqlQuery.GetPaged<IExampleEnvironment>(_container, context.Who, info, skip, ref count, retCount, filterBy, orderBy, database);
    		        foreach (var entity in entityList)
    		        {
    		            list.Add(entity as IExampleEnvironment);
    		        }
    
    		        return list;
    		    }
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentGetFiltered"))
    			{
    				command.AddInParameter("@skip", skip);			
    				command.AddInParameter("@retcount", retCount);
    				if(!string.IsNullOrEmpty(filterBy)) command.AddInParameter("@filterBy", filterBy);
    				command.AddInParameter("@orderBy", orderBy);
        			command.AddParameter("@count", ParameterDirection.InputOutput, count);
    			
    				command.ExecuteReader(dataReader =>
    					{
                            IExampleEnvironment item = _factory.CreateNew(info) as IExampleEnvironment;
    						while(SqlQuery.PopulateEntity(context, item, dataReader))
    						{
    							list.Add(item);
    	                        item = _factory.CreateNew(info) as IExampleEnvironment;
    						} 
    					});
    
    				if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
    			}
    			return list;
    		}
    	}
    
    	public void Add(IEntityRequestContext context, IExampleEnvironment  entity)
    	{ 
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Add, entity, null ,context)) {
    
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentAdd"))
    			{
    				buildEnvironmentCommandParameters(context, entity, command, true );
    				command.ExecuteNonQuery();
    			}
    		}
    	}
    
    	public void Modify(IEntityRequestContext context, IExampleEnvironment  before, IExampleEnvironment  after)
    	{ 
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Modify, before, after, context)) {
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentModify"))
    			{
    				buildEnvironmentCommandParameters(context, before, command, false );
    				command.ExecuteNonQuery();
    			}
    		}
    	}
    
    	public void Remove(IEntityRequestContext context, IExampleEnvironment  entity )
    	{
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Remove, entity, null, context)) {
    
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentRemove"))
    			{
    				buildEnvironmentCommandParameters(context, entity, command, false );
    				command.ExecuteNonQuery();
    			}
    		}
    	}
    
    	static void buildEnvironmentCommandParameters(IEntityRequestContext context, IExampleEnvironment entity, IDBCommand command, bool addRecord )
    	{ 
            Debug.Assert(command!=null);
       		if(addRecord) command.AddParameter("@id", ParameterDirection.InputOutput,  entity.ID);
    		else command.AddInParameter("@id", entity.ID);
    		command.AddInParameter("@name", T4Config.CheckUpperCase("dbo","environment","name",entity.Name, false));
    	}
    }
}

