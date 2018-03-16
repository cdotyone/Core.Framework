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
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Civic.Core.Data;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Configuration;
using Civic.Framework.WebApi.Test.Entities;

using Entity1Entity = Civic.Framework.WebApi.Test.Entities.Entity1;
namespace Civic.Framework.WebApi.Test.Data
{
    internal partial class ExampleData
    {
    
    	internal static Entity1Entity GetEntity1( String name, IDBConnection database)
    	{
            Debug.Assert(database!=null);
    
    		var entity1Returned = new Entity1Entity();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Get"))
    		{
    			command.AddInParameter("@name", name);
    			
                command.ExecuteReader(dataReader =>
                    {
                        if (populateEntity1(entity1Returned, dataReader))
                        {
    					entity1Returned.Name = name;
    					                    }
                        else entity1Returned = null;
                    });
    		}
    
    		return entity1Returned;
    	}
    
    	internal static List<Entity1Entity> GetPagedEntity1(int skip, ref int count, bool retCount, string filterBy, string orderBy, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		var list = new List<Entity1Entity>();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1GetFiltered"))
    		{
                command.AddInParameter("@skip", skip);			
                command.AddInParameter("@retcount", retCount);
    			if(!string.IsNullOrEmpty(filterBy)) command.AddInParameter("@filterBy", filterBy);
    			command.AddInParameter("@orderBy", orderBy);
        		command.AddParameter("@count", ParameterDirection.InputOutput, count);
    			
                command.ExecuteReader(dataReader =>
                    {
    					var item = new Entity1Entity();
    					while(populateEntity1(item, dataReader))
    					{
    						list.Add(item);
    						item = new Entity1Entity();
    					} 
                    });
    
    			if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
    		}
    
    		return list;
    	}
    
    	internal static void AddEntity1(Entity1Entity entity1, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Add"))
    		{
    			buildEntity1CommandParameters( entity1, command, true );
    			command.ExecuteNonQuery();
    		}
    	}
    
    	internal static List<Entity1Entity> ModifyEntity1(Entity1Entity entity1, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		var list = new List<Entity1Entity>();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Modify"))
    		{
    			buildEntity1CommandParameters( entity1, command, false );
    			command.ExecuteNonQuery();
    		}
    
    		return list;
    	}
    
    	internal static void RemoveEntity1( String name, IDBConnection database )
    	{
            Debug.Assert(database!=null);
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Remove"))
    		{
    			command.AddInParameter("@name", name);
    			command.ExecuteNonQuery();
    		}
    	}
    
    	private static void buildEntity1CommandParameters( Entity1Entity entity, IDBCommand command, bool addRecord )
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
    	
    	private static bool populateEntity1(Entity1Entity entity, IDataReader dataReader)
    	{
    		if (dataReader==null || !dataReader.Read()) return false;
    							
    		entity.Name = dataReader["Name"] != null && !string.IsNullOrEmpty(dataReader["Name"].ToString()) ? dataReader["Name"].ToString() : string.Empty;						
    		entity.EnvironmentID = dataReader["EnvironmentID"] != null && !(dataReader["EnvironmentID"] is DBNull) ? Int32.Parse(dataReader["EnvironmentID"].ToString()) : 0;					
    		if(!(dataReader["Dte"] is DBNull)) entity.Dte = DateTime.Parse(dataReader["Dte"].ToString()).FromDB();					
    		if(!(dataReader["Dte2"] is DBNull)) entity.Dte2 = DateTime.Parse(dataReader["Dte2"].ToString()).FromDB();						
    		entity.Dble1 = double.Parse(dataReader["Dble1"] != null && !(dataReader["Dble1"] is DBNull) && dataReader["Dble1"] != null ? dataReader["Dble1"].ToString() : "0");						
    		entity.Dec1 = double.Parse(dataReader["Dec1"] != null && !(dataReader["Dec1"] is DBNull) && dataReader["Dec1"] != null ? dataReader["Dec1"].ToString() : "0");		
    			return true;
    	}
    }
}

