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
using Civic.T4.Entities;

using Entity1Entity = Civic.T4.Entities.Entity1;

namespace Civic.T4.Services
{

    public partial class ExampleService
    {
        public Entity1Entity GetEntity1ByName(String name)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetEntity1ByName"))
            {

                try
                {
                    return Data.ExampleData.GetEntity1(name, null);
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }

            return null;
        }

        public List<Entity1Entity> GetPagedEntity1(int skip, ref int count, bool retCount, string filterBy, string orderBy)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetPagedEntity1"))
            {

                try
                {
                    return Data.ExampleData.GetPagedEntity1(skip, ref count, retCount, filterBy, orderBy, null);
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }

            return null;
        }

        public void AddEntity1(Entity1Entity entity1)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "AddEntity1"))
            {

                try
                {
                    var db = Data.ExampleData.GetConnection();
                    var logid = AuditManager.LogAdd(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", entity1.Name.ToString() + "", entity1);
                    Data.ExampleData.AddEntity1(entity1, db);
                    AuditManager.MarkSuccessFul(logid);
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }
        }

        public void ModifyEntity1(Entity1Entity entity1)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "ModifyEntity1"))
            {

                try
                {
                    var db = Data.ExampleData.GetConnection();
                    var before = Data.ExampleData.GetEntity1(entity1.Name, db);
                    var logid = AuditManager.LogModify(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", before.Name.ToString() + "", before, entity1);
                    Data.ExampleData.ModifyEntity1(entity1, db);
                    AuditManager.MarkSuccessFul(logid);
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }
        }

        public void RemoveEntity1(String name)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "RemoveEntity1"))
            {

                try
                {
                    var db = Data.ExampleData.GetConnection();
                    var before = Data.ExampleData.GetEntity1(name, db);
                    var logid = AuditManager.LogRemove(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", before.Name.ToString() + "", before);
                    Data.ExampleData.RemoveEntity1(name, db);
                    AuditManager.MarkSuccessFul(logid);
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }
        }

    }

}
