﻿
//------------------------------------------------------------------------------
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
using Civic.Framework.WebApi.Test.Interfaces;


using IExampleEntity3 = Civic.Framework.WebApi.Test.Interfaces.IEntity3;

namespace Civic.Framework.WebApi.Test.Data.SqlServer
{

public partial class Entity3Repository : IEntityRepository<IExampleEntity3>
{

    Container _container;
	private readonly IEntityCreateFactory _factory;
    private readonly IEntityInfo _info;

    public Entity3Repository(Container container, IEntityCreateFactory factory)
    {
        _container = container;
		_factory = factory;
        _info = _container.GetInstance<IExampleEntity3>().GetInfo();
    }

	public IExampleEntity3 Get(IEntityRequestContext context,  IExampleEntity3 entity)
	{
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Get, null, null ,context)) {

			Debug.Assert(database!=null);

			var info = entity.GetInfo();

		    if (!info.UseProcedureGet)
		    {
		        return SqlQuery.Get(_container, context.Who, entity, database);
		    }

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3Get"))
			{
				command.AddInParameter("@someUID", entity.SomeUID);
				

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

	public IEnumerable<IExampleEntity3> GetPaged(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy)
	{ 
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Get, null, null ,context)) {

			Debug.Assert(database!=null);

			var list = new List<IExampleEntity3>();

		    if (!_info.UseProcedureGet)
		    {
		        var entityList = SqlQuery.GetPaged<IExampleEntity3>(_container, context.Who, _info, skip, ref count, retCount, filterBy, orderBy, database);
		        foreach (var entity in entityList)
		        {
		            list.Add(entity as IExampleEntity3);
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
                        var item = _container.GetInstance<IExampleEntity3>();
						while(SqlQuery.PopulateEntity(context, item, dataReader))
						{
							list.Add(item);
	                        item = _container.GetInstance<IExampleEntity3>();
						} 
					});

				if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
			}
			return list;
		}
	}

	public void Add(IEntityRequestContext context, IExampleEntity3  entity)
	{ 
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Add, entity, null ,context)) {

			Debug.Assert(database!=null);

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3Add"))
			{
				buildEntity3CommandParameters(context, entity, command, true );
				command.ExecuteNonQuery();
			}
		}
	}

	public void Modify(IEntityRequestContext context, IExampleEntity3  before, IExampleEntity3  after)
	{ 
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Modify, before, after, context)) {
			Debug.Assert(database!=null);

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3Modify"))
			{
				buildEntity3CommandParameters(context, before, command, false );
				command.ExecuteNonQuery();
			}
		}
	}

	public void Remove(IEntityRequestContext context, IExampleEntity3  entity )
	{
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Remove, entity, null, context)) {

			Debug.Assert(database!=null);

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3Remove"))
			{
				buildEntity3CommandParameters(context, entity, command, false );
				command.ExecuteNonQuery();
			}
		}
	}

	static void buildEntity3CommandParameters(IEntityRequestContext context, IExampleEntity3 entity, IDBCommand command, bool addRecord )
	{ 
        Debug.Assert(command!=null);

		command.AddInParameter("@someuid", entity.SomeUID.ToUpper());

   		if(addRecord) command.AddParameter("@someid", ParameterDirection.InputOutput,  entity.SomeID);
		else command.AddInParameter("@someid", entity.SomeID);

		command.AddInParameter("@otherdate", entity.OtherDate.ToDB());

	}
}

}

