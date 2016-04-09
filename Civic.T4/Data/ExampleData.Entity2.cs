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
using Civic.T4.WebApi;
using Civic.T4.WebApi.Configuration;
using Civic.T4.Entities;

using Entity2Entity = Civic.T4.Entities.Entity2;
namespace Civic.T4.Data
{
    internal partial class ExampleData
    {

        internal static Entity2Entity GetEntity2(Int32 id, String ff, IDBConnection database)
        {
            Debug.Assert(database != null);

            var entity2Returned = new Entity2Entity();

            using (var command = database.CreateStoredProcCommand("dbo", "usp_Entity2Get"))
            {
                command.AddInParameter("@id", id);
                command.AddInParameter("@ff", ff);

                command.ExecuteReader(dataReader =>
                    {
                        if (populateEntity2(entity2Returned, dataReader))
                        {
                            entity2Returned.Id = id;
                            entity2Returned.ff = ff;
                        }
                        else entity2Returned = null;
                    });
            }

            return entity2Returned;
        }

        internal static List<Entity2Entity> GetPagedEntity2(int skip, ref int count, bool retCount, string filterBy, string orderBy, IDBConnection database)
        {
            Debug.Assert(database != null);

            var list = new List<Entity2Entity>();

            using (var command = database.CreateStoredProcCommand("dbo", "usp_Entity2GetFiltered"))
            {
                command.AddInParameter("@skip", skip);
                command.AddInParameter("@retcount", retCount);
                if (!string.IsNullOrEmpty(filterBy)) command.AddInParameter("@filterBy", filterBy);
                command.AddInParameter("@orderBy", orderBy);
                command.AddParameter("@count", ParameterDirection.InputOutput, count);

                command.ExecuteReader(dataReader =>
                    {
                        var item = new Entity2Entity();
                        while (populateEntity2(item, dataReader))
                        {
                            list.Add(item);
                            item = new Entity2Entity();
                        }
                    });

                if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
            }

            return list;
        }

        internal static void AddEntity2(Entity2Entity entity2, IDBConnection database)
        {
            Debug.Assert(database != null);

            using (var command = database.CreateStoredProcCommand("dbo", "usp_Entity2Add"))
            {
                buildEntity2CommandParameters(entity2, command, true);
                command.ExecuteNonQuery();
                entity2.Id = Int32.Parse(
                command.GetOutParameter("@id").Value.ToString());
            }
        }

        internal static List<Entity2Entity> ModifyEntity2(Entity2Entity entity2, IDBConnection database)
        {
            Debug.Assert(database != null);

            var list = new List<Entity2Entity>();

            using (var command = database.CreateStoredProcCommand("dbo", "usp_Entity2Modify"))
            {
                buildEntity2CommandParameters(entity2, command, false);
                command.ExecuteNonQuery();
            }

            return list;
        }

        internal static void RemoveEntity2(Int32 id, String ff, IDBConnection database)
        {
            Debug.Assert(database != null);

            using (var command = database.CreateStoredProcCommand("dbo", "usp_Entity2Remove"))
            {
                command.AddInParameter("@id", id);
                command.AddInParameter("@ff", ff);
                command.ExecuteNonQuery();
            }
        }

        private static void buildEntity2CommandParameters(Entity2Entity entity2, IDBCommand command, bool addRecord)
        {
            Debug.Assert(command != null);
            if (addRecord) command.AddParameter("@id", ParameterDirection.InputOutput, entity2.Id);
            else command.AddInParameter("@id", entity2.Id);
            if (addRecord) command.AddParameter("@ff", ParameterDirection.InputOutput, T4Config.CheckUpperCase("dbo", "entity2", "ff", entity2.ff));
            else command.AddInParameter("@ff", T4Config.CheckUpperCase("dbo", "entity2", "ff", entity2.ff));

        }

        private static bool populateEntity2(Entity2Entity entity2, IDataReader dataReader)
        {
            if (dataReader == null || !dataReader.Read()) return false;

            entity2.Id = dataReader["Id"] != null && !(dataReader["Id"] is DBNull) ? Int32.Parse(dataReader["Id"].ToString()) : 0;
            entity2.ff = dataReader["ff"] != null && !string.IsNullOrEmpty(dataReader["ff"].ToString()) ? dataReader["ff"].ToString() : string.Empty;
            return true;
        }
    }
}

