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

using EnvironmentEntity = Civic.Framework.WebApi.Test.Entities.Environment;
namespace Civic.Framework.WebApi.Test.Data
{
    internal partial class ExampleData
    {
    
    	internal static EnvironmentEntity GetEnvironment( Int32 id, IDBConnection database)
    	{
            Debug.Assert(database!=null);
    
    		var environmentReturned = new EnvironmentEntity();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentGet"))
    		{
    			command.AddInParameter("@id", id);
    			
                command.ExecuteReader(dataReader =>
                    {
                        if (populateEnvironment(environmentReturned, dataReader))
                        {
    					environmentReturned.ID = id;
    					                    }
                        else environmentReturned = null;
                    });
    		}
    
    		return environmentReturned;
    	}
    
    	internal static List<EnvironmentEntity> GetPagedEnvironment(int skip, ref int count, bool retCount, string filterBy, string orderBy, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		var list = new List<EnvironmentEntity>();
    
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
    					while(populateEnvironment(item, dataReader))
    					{
    						list.Add(item);
    						item = new EnvironmentEntity();
    					} 
                    });
    
    			if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
    		}
    
    		return list;
    	}
    
    	internal static int AddEnvironment(EnvironmentEntity environment, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentAdd"))
    		{
    			buildEnvironmentCommandParameters( environment, command, true );
    			command.ExecuteNonQuery();
    			 
    			return environment.ID = Int32.Parse(
    			command.GetOutParameter("@id").Value.ToString());
    		}
    	}
    
    	internal static List<EnvironmentEntity> ModifyEnvironment(EnvironmentEntity environment, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		var list = new List<EnvironmentEntity>();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentModify"))
    		{
    			buildEnvironmentCommandParameters( environment, command, false );
    			command.ExecuteNonQuery();
    		}
    
    		return list;
    	}
    
    	internal static void RemoveEnvironment( Int32 id, IDBConnection database )
    	{
            Debug.Assert(database!=null);
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentRemove"))
    		{
    			command.AddInParameter("@id", id);
    			command.ExecuteNonQuery();
    		}
    	}
    
    	private static void buildEnvironmentCommandParameters( EnvironmentEntity environment, IDBCommand command, bool addRecord )
    	{ 
            Debug.Assert(command!=null);
       		if(addRecord) command.AddParameter("@id", ParameterDirection.InputOutput,  environment.ID);
    		else command.AddInParameter("@id", environment.ID);
    		command.AddInParameter("@name", T4Config.CheckUpperCase("dbo","environment","name",environment.Name));
    
    	}
    	
    	private static bool populateEnvironment(EnvironmentEntity environment, IDataReader dataReader)
    	{
    		if (dataReader==null || !dataReader.Read()) return false;
    								
    		environment.ID = dataReader["ID"] != null && !(dataReader["ID"] is DBNull) ? Int32.Parse(dataReader["ID"].ToString()) : 0;					
    		environment.Name = dataReader["Name"] != null && !string.IsNullOrEmpty(dataReader["Name"].ToString()) ? dataReader["Name"].ToString() : string.Empty;		
    			return true;
    		}
    	}
}

