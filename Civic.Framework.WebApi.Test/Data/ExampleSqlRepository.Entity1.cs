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

using Entity1Entity = Civic.Framework.WebApi.Test.Entities.Entity1;
namespace Civic.Framework.WebApi.Test.Data
{
    public partial class ExampleSqlRepository
    {
    	public Entity1Entity GetEntity1(IEntityRequestContext context,  String name)
    	{
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Get, null ,context)) {
    
    			Debug.Assert(database!=null);
    
       			var retval = Container.GetInstance<Entity1Entity>();
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Get"))
    			{
    		        var info = Entity1Entity.Info;
    		        if (!info.UseProcedureGet)
    		        {
    		            return SqlQuery.Get(Container, context.Who,  name, Entity1Entity.Info, database) as Entity1Entity;
                    }
    
    				command.AddInParameter("@name", name);
    				
    				command.ExecuteReader(dataReader =>
    					{
    						if (SqlQuery.PopulateEntity(context, retval, dataReader))
    						{
    							retval.Name = name;
    												}
    						else retval = null;
    					});
    			}
    			return retval;
    		}
    	}
    
    	public List<Entity1Entity> GetPagedEntity1(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy)
    	{ 
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Get, null ,context)) {
    
    			Debug.Assert(database!=null);
    
    			var list = new List<Entity1Entity>();
    
    		    var info = Entity1Entity.Info;
    		    if (!info.UseProcedureGet)
    		    {
    		        var entityList = SqlQuery.GetPaged(Container, context.Who, Entity1Entity.Info, skip, ref count, retCount, filterBy, orderBy, database);
    		        foreach (var entity in entityList)
    		        {
    		            list.Add(entity as Entity1Entity);
    		        }
    
    		        return list;
    		    }
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1GetFiltered"))
    			{
    				command.AddInParameter("@skip", skip);			
    				command.AddInParameter("@retcount", retCount);
    				if(!string.IsNullOrEmpty(filterBy)) command.AddInParameter("@filterBy", filterBy);
    				command.AddInParameter("@orderBy", orderBy);
        			command.AddParameter("@count", ParameterDirection.InputOutput, count);
    			
    				command.ExecuteReader(dataReader =>
    					{
       						var item = Container.GetInstance<Entity1Entity>();
    						while(SqlQuery.PopulateEntity(context, item, dataReader))
    						{
    							list.Add(item);
       							item = Container.GetInstance<Entity1Entity>();
    						} 
    					});
    
    				if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
    			}
    			return list;
    		}
    	}
    
    	public void AddEntity1(IEntityRequestContext context, Entity1Entity entity)
    	{ 
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Add, entity ,context)) {
    
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Add"))
    			{
    				buildEntity1CommandParameters(context, entity, command, true );
    				command.ExecuteNonQuery();
    			}
    
    		    context.Operations.Add(new SqlOperation
    		    {
    		        Type = EntityOperationType.Modify,
    		        DbCode = database.DBCode,
    		        Connection = database,
    		        Entity = entity
    		    });
    		}
    	}
    
    	public void ModifyEntity1(IEntityRequestContext context, Entity1Entity before, Entity1Entity after)
    	{ 
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Modify, before, context)) {
    			Debug.Assert(database!=null);
    
    			context.Operations.Add(new SqlOperation {
    			});
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Modify"))
    			{
    				buildEntity1CommandParameters(context, before, command, false );
    				command.ExecuteNonQuery();
    			}
    
    		    context.Operations.Add(new SqlOperation
    		    {
                    Type = EntityOperationType.Modify,
                    DbCode = database.DBCode,
                    Connection = database,
                    Entity = before
    		    });
    		}
    	}
    
    	public void RemoveEntity1(IEntityRequestContext context, Entity1Entity entity )
    	{
    		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Remove, entity, context)) {
    
    			Debug.Assert(database!=null);
    
    			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Remove"))
    			{
    				buildEntity1CommandParameters(context, entity, command, false );
    				command.ExecuteNonQuery();
    			}
    
    		    context.Operations.Add(new SqlOperation
    		    {
    		        Type = EntityOperationType.Remove,
    		        DbCode = database.DBCode,
    		        Connection = database,
    		        Entity = entity
    		    });
    		}
    	}
    
    	static void buildEntity1CommandParameters(IEntityRequestContext context, Entity1Entity entity, IDBCommand command, bool addRecord )
    	{ 
            Debug.Assert(command!=null);
       		if(addRecord) command.AddParameter("@name", ParameterDirection.InputOutput,  T4Config.CheckUpperCase("dbo","entity1","name",entity.Name));
    		else command.AddInParameter("@name", T4Config.CheckUpperCase("dbo","entity1","name",entity.Name));
    		command.AddInParameter("@environmentid", entity.EnvironmentID);
    		command.AddInParameter("@dte", entity.Dte.ToDB());
    		command.AddInParameter("@dte2", entity.Dte2.ToDB());
    		command.AddInParameter("@dble1", entity.Dble1);
    		command.AddInParameter("@dec1", entity.Dec1);
    	}
    }
}

