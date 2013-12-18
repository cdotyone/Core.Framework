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
using Civic.Core.Logging;
using Civic.T4.Entities;

using EnvironmentEntity = Civic.T4.Entities.Environment;

namespace Civic.T4.Services
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class ExampleService : IExample
    {

        public EnvironmentEntity GetEnvironmentById(int id, string fillProperties)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetEnvironmentById"))
            {

                try
                {
                    return Data.ExampleData.GetEnvironment(id, string.IsNullOrEmpty(fillProperties) ? null : fillProperties.Split(','));
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }

            return null;
        }

        public List<EnvironmentEntity> GetPagedEnvironment(int skip, ref int count, bool retCount, string filterBy, string orderBy, string fillProperties)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetPagedEnvironment"))
            {

                try
                {
                    return Data.ExampleData.GetPagedEnvironment(skip, ref count, retCount, filterBy, orderBy, string.IsNullOrEmpty(fillProperties) ? null : fillProperties.Split(','));
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }

            return null;
        }

        public int AddEnvironment(EnvironmentEntity environment)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "AddEnvironment"))
            {

                try
                {
                    return Data.ExampleData.AddEnvironment(environment);
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }

            return -1;
        }

        public void ModifyEnvironment(EnvironmentEntity environment)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "ModifyEnvironment"))
            {

                try
                {
                    Data.ExampleData.ModifyEnvironment(environment);
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }
        }

        public void RemoveEnvironment(int id)
        {
            using (Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "RemoveEnvironment"))
            {

                try
                {
                    Data.ExampleData.RemoveEnvironment(id);
                }
                catch (Exception ex)
                {
                    if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
                }

            }
        }


    }
}

