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
using Civic.Core.Data;
using Civic.Framework.WebApi.Configuration;
using Civic.Framework.WebApi.Test.Entities;
using Civic.Framework.WebApi.Test.Interfaces;

using EnvironmentEntity = Civic.Framework.WebApi.Test.Entities.Environment;
namespace Civic.Framework.WebApi.Test.Data
{
    public partial class ExampleSqlRepository
    {
    	public EnvironmentEntity GetEnvironment(IEntityRequestContext context,  Int32 id)
    	{
    		using(var database = SqlQuery.GetConnection("dbo", EntityOperationType.Get, null ,context)) {
    
    			Debug.Assert(database!=null);
    
    			var retval = new EnvironmentEntity();
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentGet"))
    			{
    		        var info = EnvironmentEntity.Info;
    		        if (!info.UseProcedureGet)
    		        {
    		            return SqlQuery.Get(Container, context.Who,  id.ToString(), EnvironmentEntity.Info, database) as EnvironmentEntity;
                    }
    
    				command.AddInParameter("@id", id);
    				
    				command.ExecuteReader(dataReader =>
    					{
    						if (populateEnvironment(context, retval, dataReader))
    						{
    							retval.ID = id;
    												}
    						else retval = null;
    					});
    			}
    			return retval;
    		}
    	}
    
    	public List<EnvironmentEntity> GetPagedEnvironment(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy)
    	{ 
    		using(var database = SqlQuery.GetConnection("dbo", EntityOperationType.Get, null ,context)) {
    
    			Debug.Assert(database!=null);
    
    			var list = new List<EnvironmentEntity>();
    
    		    var info = EnvironmentEntity.Info;
    		    if (!info.UseProcedureGet)
    		    {
    		        var entityList = SqlQuery.GetPaged(Container, context.Who, EnvironmentEntity.Info, skip, ref count, retCount, filterBy, orderBy, database);
    		        foreach (var entity in entityList)
    		        {
    		            list.Add(entity as EnvironmentEntity);
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
    						var item = new EnvironmentEntity();
    						while(populateEnvironment(context, item, dataReader))
    						{
    							list.Add(item);
    							item = new EnvironmentEntity();
    						} 
    					});
    
    				if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
    			}
    			return list;
    		}
    	}
    
    	public void AddEnvironment(IEntityRequestContext context, EnvironmentEntity entity)
    	{ 
    		using(var database = SqlQuery.GetConnection("dbo", EntityOperationType.Add, entity ,context)) {
    
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentAdd"))
    			{
    				buildEnvironmentCommandParameters(context, entity, command, true );
    				command.ExecuteNonQuery();
    			}
    		}
    	}
    
    	public void ModifyEnvironment(IEntityRequestContext context, EnvironmentEntity entity)
    	{ 
    		using(var database = SqlQuery.GetConnection("dbo", EntityOperationType.Modify, entity, context)) {
    			Debug.Assert(database!=null);
    
    			context.Operations.Add(new SqlOperation {
    			});
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentModify"))
    			{
    				buildEnvironmentCommandParameters(context, entity, command, false );
    				command.ExecuteNonQuery();
    			}
    		}
    	}
    
    	public void RemoveEnvironment(IEntityRequestContext context, EnvironmentEntity entity )
    	{
    		using(var database = SqlQuery.GetConnection("dbo", EntityOperationType.Remove, entity, context)) {
    
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentRemove"))
    			{
    				buildEnvironmentCommandParameters(context, entity, command, false );
    				command.ExecuteNonQuery();
    			}
    		}
    	}
    
    	static void buildEnvironmentCommandParameters(IEntityRequestContext context, EnvironmentEntity entity, IDBCommand command, bool addRecord )
    	{ 
            Debug.Assert(command!=null);
       		if(addRecord) command.AddParameter("@id", ParameterDirection.InputOutput,  entity.ID);
    		else command.AddInParameter("@id", entity.ID);
    		command.AddInParameter("@name", T4Config.CheckUpperCase("dbo","environment","name",entity.Name, false));
    
    	}
    	
    	private static bool populateEnvironment(IEntityRequestContext context, EnvironmentEntity entity, IDataReader dataReader)
    	{
    		if (dataReader==null || !dataReader.Read()) return false;
    								
    		entity.ID = dataReader["ID"] != null && !(dataReader["ID"] is DBNull) ? Int32.Parse(dataReader["ID"].ToString()) : 0;					
    		entity.Name = dataReader["Name"] != null && !string.IsNullOrEmpty(dataReader["Name"].ToString()) ? dataReader["Name"].ToString() : string.Empty;		
    
    		return true;
    	}
    }
}

