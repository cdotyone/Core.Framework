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
using System.ServiceModel.Activation;
using System.Collections.Generic;
using Civic.Core.Framework.Security;
using Civic.Core.Audit;
using Civic.Core.Logging;
using Civic.Framework.WebApi.Test.Entities;

using Entity2Entity = Civic.Framework.WebApi.Test.Entities.Entity2;

namespace Civic.Framework.WebApi.Test.Services
{

    public partial class ExampleService
    {
        public Entity2Entity GetEntity2(Int32 someID, String ff)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetEntity2Byff"))
            {

                try
                {
                    using (var database = Connection)
                    {
                        return Data.ExampleData.GetEntity2(someID, ff, database);
                    }
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }

            return null;
        }

        public List<Entity2Entity> GetPagedEntity2(int skip, ref int count, bool retCount, string filterBy, string orderBy)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetPagedEntity2"))
            {

                try
                {
                    using (var database = Connection)
                    {
                        return Data.ExampleData.GetPagedEntity2(skip, ref count, retCount, filterBy, orderBy, database);
                    }
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }

            return null;
        }

        public void AddEntity2(Entity2Entity entity2)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "AddEntity2"))
            {

                try
                {
                    using (var db = Connection)
                    {
                        var logid = AuditManager.LogAdd(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", entity2.SomeID.ToString() + entity2.ff.ToString() + "", entity2);
                        Data.ExampleData.AddEntity2(entity2, db);
                        AuditManager.MarkSuccessFul("dbo", logid);
                    }
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }
        }

        public void ModifyEntity2(Entity2Entity entity2)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "ModifyEntity2"))
            {

                try
                {
                    using (var db = Connection)
                    {
                        var before = Data.ExampleData.GetEntity2(entity2.SomeID, entity2.ff, db);
                        var logid = AuditManager.LogModify(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.SomeID.ToString() + before.ff.ToString() + "", before, entity2);
                        Data.ExampleData.ModifyEntity2(entity2, db);
                        AuditManager.MarkSuccessFul("dbo", logid);
                    }
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }
        }

        public void RemoveEntity2(Int32 someID, String ff)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "RemoveEntity2"))
            {

                try
                {
                    using (var db = Connection)
                    {
                        var before = Data.ExampleData.GetEntity2(someID, ff, db);
                        var logid = AuditManager.LogRemove(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.SomeID.ToString() + before.ff.ToString() + "", before);
                        Data.ExampleData.RemoveEntity2(someID, ff, db);
                        AuditManager.MarkSuccessFul("dbo", logid);
                    }
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }
        }

    }

}
