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

using Entity3Entity = Civic.Framework.WebApi.Test.Entities.Entity3;
namespace Civic.Framework.WebApi.Test.Data
{
    public partial class ExampleSqlRepository
    {
    	public Entity3Entity GetEntity3(IEntityRequestContext context,  String someUID)
    	{
    		using(var database = GetConnection(context)) {
    
    			Debug.Assert(database!=null);
    
    			var retval = new Entity3Entity();
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3Get"))
    			{
    		        var info = Entity3Entity.Info;
    		        if (!info.UseProcedureGet)
    		        {
    		            return SqlQuery.Get(Container, context.Who,  someUID, Entity3Entity.Info, database) as Entity3Entity;
                    }
    
    				command.AddInParameter("@someUID", someUID);
    				
    				command.ExecuteReader(dataReader =>
    					{
    						if (populateEntity3(context, retval, dataReader))
    						{
    							retval.SomeUID = someUID;
    												}
    						else retval = null;
    					});
    			}
    			return retval;
    		}
    	}
    
    	public List<Entity3Entity> GetPagedEntity3(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy)
    	{ 
    		using(var database = GetConnection(context)) {
    
    			Debug.Assert(database!=null);
    
    			var list = new List<Entity3Entity>();
    
    		    var info = Entity3Entity.Info;
    		    if (!info.UseProcedureGet)
    		    {
    		        var entityList = SqlQuery.GetPaged(Container, context.Who, Entity3Entity.Info, skip, ref count, retCount, filterBy, orderBy, database);
    		        foreach (var entity in entityList)
    		        {
    		            list.Add(entity as Entity3Entity);
    		        }
    
    		        return list;
    		    }
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3GetFiltered"))
    			{
    				command.AddInParameter("@skip", skip);			
    				command.AddInParameter("@retcount", retCount);
    				if(!string.IsNullOrEmpty(filterBy)) command.AddInParameter("@filterBy", filterBy);
    				command.AddInParameter("@orderBy", orderBy);
        			command.AddParameter("@count", ParameterDirection.InputOutput, count);
    			
    				command.ExecuteReader(dataReader =>
    					{
    						var item = new Entity3Entity();
    						while(populateEntity3(context, item, dataReader))
    						{
    							list.Add(item);
    							item = new Entity3Entity();
    						} 
    					});
    
    				if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
    			}
    			return list;
    		}
    	}
    
    	public void AddEntity3(IEntityRequestContext context, Entity3Entity entity3)
    	{ 
    		using(var database = GetConnection(context)) {
    
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3Add"))
    			{
    				buildEntity3CommandParameters(context, entity3, command, true ); 
    				entity3.SomeUID = entity3.SomeUID.InsureUID(); 
    				command.ExecuteNonQuery();
    			}
    		}
    	}
    
    	public void ModifyEntity3(IEntityRequestContext context, Entity3Entity entity3)
    	{ 
    		using(var database = GetConnection(context)) {
    
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3Modify"))
    			{
    				buildEntity3CommandParameters(context, entity3, command, false );
    				command.ExecuteNonQuery();
    			}
    		}
    	}
    
    	public void RemoveEntity3(IEntityRequestContext context,  String someUID )
    	{
    		using(var database = GetConnection(context)) {
    
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3Remove"))
    			{
    				command.AddInParameter("@someUID", someUID);
    					command.ExecuteNonQuery();
    			}
    
    		}
    	}
    
    	static void buildEntity3CommandParameters(IEntityRequestContext context, Entity3Entity entity, IDBCommand command, bool addRecord )
    	{ 
            Debug.Assert(command!=null);
    		command.AddInParameter("@someuid", entity.SomeUID.ToUpper());
       		if(addRecord) command.AddParameter("@someid", ParameterDirection.InputOutput,  entity.SomeID);
    		else command.AddInParameter("@someid", entity.SomeID);
    		command.AddInParameter("@otherdate", entity.OtherDate.ToDB());
    
    	}
    	
    	private static bool populateEntity3(IEntityRequestContext context, Entity3Entity entity, IDataReader dataReader)
    	{
    		if (dataReader==null || !dataReader.Read()) return false;
    							
    		entity.SomeUID = dataReader["SomeUID"] != null && !string.IsNullOrEmpty(dataReader["SomeUID"].ToString()) ? dataReader["SomeUID"].ToString() : string.Empty;						
    		entity.SomeID = dataReader["SomeID"] != null && !(dataReader["SomeID"] is DBNull) ? Int64.Parse(dataReader["SomeID"].ToString()) : 0;					
    		if(!(dataReader["Modified"] is DBNull)) entity.Modified = DateTime.Parse(dataReader["Modified"].ToString()).FromDB();					
    		if(!(dataReader["OtherDate"] is DBNull)) entity.OtherDate = DateTime.Parse(dataReader["OtherDate"].ToString()).FromDB();		
    
    		return true;
    	}
    }
}

