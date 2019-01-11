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
using Framework.Core;
using Framework.Core.Configuration;
using Framework.Core.Test.Interfaces;


using IExampleEntity1 = Framework.Core.Test.Interfaces.IEntity1;
namespace Framework.Core.Test.Data.SqlServer
{
    
public partial class Entity1Repository : IEntityRepository<IExampleEntity1>
{

    Container _container;
	private readonly IEntityCreateFactory _factory;
    private readonly IEntityInfo _info;

    public Entity1Repository(Container container, IEntityCreateFactory factory)
    {
        _container = container;
		_factory = factory;
        _info = _container.GetInstance<IExampleEntity1>().GetInfo();
    }

	public IExampleEntity1 Get(IEntityRequestContext context,  IExampleEntity1 entity)
	{
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Get, null, null ,context)) {

			Debug.Assert(database!=null);

			var info = entity.GetInfo();

		    if (!info.UseProcedureGet)
		    {
		        return SqlQuery.Get(_container, context.Who, entity, database);
		    }

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Get"))
			{
				buildCommand(context, entity, command, false );

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

	public IEnumerable<IExampleEntity1> GetPaged(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy)
	{ 
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Get, null, null ,context)) {

			Debug.Assert(database!=null);

			var list = new List<IExampleEntity1>();

		    if (!_info.UseProcedureGet)
		    {
		        var entityList = SqlQuery.GetPaged<IExampleEntity1>(_container, context.Who, _info, skip, ref count, retCount, filterBy, orderBy, database);
		        foreach (var entity in entityList)
		        {
		            list.Add(entity as IExampleEntity1);
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
                        var item = _container.GetInstance<IExampleEntity1>();
						while(SqlQuery.PopulateEntity(context, item, dataReader))
						{
							list.Add(item);
	                        item = _container.GetInstance<IExampleEntity1>();
						} 
					});

				if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
			}
			return list;
		}
	}

	public void Add(IEntityRequestContext context, IExampleEntity1  entity)
	{ 
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Add, entity, null ,context)) {

			Debug.Assert(database!=null);

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Add"))
			{
				buildCommand(context, entity, command, true );
				command.ExecuteNonQuery();
			}
		}
	}

	public void Modify(IEntityRequestContext context, IExampleEntity1 before, IExampleEntity1 after)
	{ 
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Modify, before, after, context)) {
			Debug.Assert(database!=null);

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Modify"))
			{
				buildCommand(context, after, command, false );
				command.ExecuteNonQuery();
			}
		}
	}

	public void Remove(IEntityRequestContext context, IExampleEntity1  entity )
	{
		using(var database = SqlQuery.GetConnection("Example", EntityOperationType.Remove, entity, null, context)) {

			Debug.Assert(database!=null);

			using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Remove"))
			{
				buildCommand(context, entity, command, false );
				command.ExecuteNonQuery();
			}
		}
	}

	static void buildCommand(IEntityRequestContext context, IExampleEntity1 entity, IDBCommand command, bool addRecord )
	{ 
        Debug.Assert(command!=null);

		command.AddInParameter("@name", T4Config.CheckUpperCase("dbo","entity1","name",entity.Name));

		command.AddInParameter("@environmentid", entity.EnvironmentID);

		command.AddInParameter("@dte", entity.Dte.ToDB());

		command.AddInParameter("@dte2", entity.Dte2.ToDB());

		command.AddInParameter("@dble1", entity.Dble1);

		command.AddInParameter("@dec1", entity.Dec1);

	}
}
}

