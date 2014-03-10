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
using Civic.Core.Data;
using Civic.T4.Entities;

using EnvironmentEntity = Civic.T4.Entities.Environment;

namespace Civic.T4.Data
{
    internal partial class ExampleData
    {

        internal static EnvironmentEntity GetEnvironment(Int32 id, IDBConnection database, string[] fillProperties = null)
        {
            var environmentReturned = new EnvironmentEntity();

            if (database == null) database = DatabaseFactory.CreateDatabase("Example");
            using (var command = database.CreateStoredProcCommand("dbo", "usp_EnvironmentGet"))
            {
                command.AddInParameter("@id", id);

                using (IDataReader dataReader = command.ExecuteReader())
                {
                    if (populateEnvironment(environmentReturned, dataReader, database, fillProperties))
                    {
                        environmentReturned.Id = id;
                    }
                    else return null;
                }
            }

            return environmentReturned;
        }

        internal static List<EnvironmentEntity> GetPagedEnvironment(int skip, ref int count, bool retCount, string filterBy, string orderBy, IDBConnection database, string[] fillProperties = null)
        {
            var list = new List<EnvironmentEntity>();

            if (database == null) database = DatabaseFactory.CreateDatabase("Example");
            using (var command = database.CreateStoredProcCommand("dbo", string.IsNullOrEmpty(filterBy) ? "usp_EnvironmentGetPaged" : "usp_EnvironmentGetFiltered"))
            {
                command.AddInParameter("@skip", skip);
                command.AddInParameter("@retcount", retCount);
                if (!string.IsNullOrEmpty(filterBy)) command.AddInParameter("@filterBy", filterBy);
                command.AddInParameter("@orderBy", orderBy);
                command.AddParameter("@count", ParameterDirection.InputOutput, count);

                using (IDataReader dataReader = command.ExecuteReader())
                {
                    var item = new EnvironmentEntity();
                    while (populateEnvironment(item, dataReader, database, fillProperties))
                    {
                        list.Add(item);
                        item = new EnvironmentEntity();
                    }
                }

                if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
            }

            return list;
        }

        internal static int AddEnvironment(EnvironmentEntity environment, IDBConnection database)
        {
            if (database == null) database = DatabaseFactory.CreateDatabase("Example");
            using (var command = database.CreateStoredProcCommand("dbo", "usp_EnvironmentAdd"))
            {
                buildEnvironmentCommandParameters(environment, command, true);
                command.ExecuteNonQuery();
                return
               environment.Id = Int32.Parse(
               command.GetOutParameter("@id").Value.ToString());
            }
        }

        internal static List<EnvironmentEntity> ModifyEnvironment(EnvironmentEntity environment, IDBConnection database)
        {
            var list = new List<EnvironmentEntity>();

            if (database == null) database = DatabaseFactory.CreateDatabase("Example");
            using (var command = database.CreateStoredProcCommand("dbo", "usp_EnvironmentModify"))
            {
                buildEnvironmentCommandParameters(environment, command, false);
                command.ExecuteNonQuery();
            }

            return list;
        }

        internal static void RemoveEnvironment(Int32 id, IDBConnection database)
        {
            if (database == null) database = DatabaseFactory.CreateDatabase("Example");
            using (var command = database.CreateStoredProcCommand("dbo", "usp_EnvironmentRemove"))
            {
                command.AddInParameter("@id", id);
                command.ExecuteNonQuery();
            }
        }

        private static void buildEnvironmentCommandParameters(EnvironmentEntity environment, IDBCommand command, bool addRecord)
        {
            if (addRecord) command.AddParameter("@id", ParameterDirection.InputOutput, environment.Id);
            else command.AddInParameter("@id", environment.Id);
            command.AddInParameter("@name", environment.Name);

        }

        private static bool populateEnvironment(EnvironmentEntity environment, IDataReader dataReader, IDBConnection database, string[] fillProperties = null)
        {
            if (dataReader == null || !dataReader.Read()) return false;

            environment.Id = dataReader["Id"] != null && !(dataReader["Id"] is DBNull) ? Int32.Parse(dataReader["Id"].ToString()) : 0;
            environment.Name = dataReader["Name"] != null && !string.IsNullOrEmpty(dataReader["Name"].ToString()) ? dataReader["Name"].ToString() : string.Empty;
            //fillCollection("environment", environment, dataReader, database, fillProperties);
            return true;
        }
    }
}

