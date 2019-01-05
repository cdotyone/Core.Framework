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


using IExampleEntity2 = Civic.Framework.WebApi.Test.Interfaces.IEntity2;

namespace Civic.Framework.WebApi.Test.Data.SqlServer
{

public partial class Entity2Repository : IEntityRepository<IExampleEntity2>
{

    Container _container;
	private readonly IEntityCreateFactory _factory;
    private readonly IEntityInfo _info;

    public Entity2Repository(Container container, IEntityCreateFactory factory)
    {
        _container = container;
		_factory = factory;
        _info = _container.GetInstance<IExampleEntity2>().GetInfo();
    }

	public IExampleEntity2 Get(IEntityRequestContext context,  IExampleEntity2 entity)
	{
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Get, null, null ,context)) {

			Debug.Assert(database!=null);

			var info = entity.GetInfo();

		    if (!info.UseProcedureGet)
		    {
		        return SqlQuery.Get(_container, context.Who, entity, database);
		    }

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity2Get"))
			{
				command.AddInParameter("@someID", entity.SomeID);
				command.AddInParameter("@ff", entity.ff);
				

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

	public IEnumerable<IExampleEntity2> GetPaged(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy)
	{ 
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Get, null, null ,context)) {

			Debug.Assert(database!=null);

			var list = new List<IExampleEntity2>();

		    if (!_info.UseProcedureGet)
		    {
		        var entityList = SqlQuery.GetPaged<IExampleEntity2>(_container, context.Who, _info, skip, ref count, retCount, filterBy, orderBy, database);
		        foreach (var entity in entityList)
		        {
		            list.Add(entity as IExampleEntity2);
		        }

		        return list;
		    }

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity2GetFiltered"))
			{
				command.AddInParameter("@skip", skip);			
				command.AddInParameter("@retcount", retCount);
				if(!string.IsNullOrEmpty(filterBy)) command.AddInParameter("@filterBy", filterBy);
				command.AddInParameter("@orderBy", orderBy);
    			command.AddParameter("@count", ParameterDirection.InputOutput, count);
			
				command.ExecuteReader(dataReader =>
					{
                        var item = _container.GetInstance<IExampleEntity2>();
						while(SqlQuery.PopulateEntity(context, item, dataReader))
						{
							list.Add(item);
	                        item = _container.GetInstance<IExampleEntity2>();
						} 
					});

				if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
			}
			return list;
		}
	}

	public void Add(IEntityRequestContext context, IExampleEntity2  entity)
	{ 
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Add, entity, null ,context)) {

			Debug.Assert(database!=null);

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity2Add"))
			{
				buildEntity2CommandParameters(context, entity, command, true );
				command.ExecuteNonQuery();
			}
		}
	}

	public void Modify(IEntityRequestContext context, IExampleEntity2  before, IExampleEntity2  after)
	{ 
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Modify, before, after, context)) {
			Debug.Assert(database!=null);

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity2Modify"))
			{
				buildEntity2CommandParameters(context, before, command, false );
				command.ExecuteNonQuery();
			}
		}
	}

	public void Remove(IEntityRequestContext context, IExampleEntity2  entity )
	{
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Remove, entity, null, context)) {

			Debug.Assert(database!=null);

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity2Remove"))
			{
				buildEntity2CommandParameters(context, entity, command, false );
				command.ExecuteNonQuery();
			}
		}
	}

	static void buildEntity2CommandParameters(IEntityRequestContext context, IExampleEntity2 entity, IDBCommand command, bool addRecord )
	{ 
        Debug.Assert(command!=null);

   		if(addRecord) command.AddParameter("@someid", ParameterDirection.InputOutput,  entity.SomeID);
		else command.AddInParameter("@someid", entity.SomeID);

   		if(addRecord) command.AddParameter("@ff", ParameterDirection.InputOutput,  T4Config.CheckUpperCase("dbo","entity2","ff",entity.ff));
		else command.AddInParameter("@ff", T4Config.CheckUpperCase("dbo","entity2","ff",entity.ff));

		command.AddInParameter("@otherdate", entity.OtherDate.ToDB());

		command.AddInParameter("@oid", entity.OID);

	}
}

}

