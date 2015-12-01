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
using Civic.Core.Audit;
using Civic.Core.Configuration;
using Civic.Core.Data;
using Civic.Core.Logging;
using Civic.T4.WebApi;
using Civic.T4.Entities;


namespace Civic.T4.Services
{


    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class ExampleService : IExample, IEntityService
    {

        private IDBConnection _connection;

        public IDBConnection Connection
        {
            get
            {
                if (_connection == null) _connection = DatabaseFactory.CreateDatabase("Example");
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }

        public INamedElement Configuration { get; set; }

        public string ModuleName { get { return "civic"; } }

        public List<string> EntitiesProvided
        {
            get
            {
                return new List<string> { "entity1", "entity2", "environment" };
            }
        }

        public IEntity Create(string name)
        {
            switch (name)
            {
                case "entity1":
                case "Entity1":
                    return new Civic.T4.Entities.Entity1();
                case "entity2":
                case "Entity2":
                    return new Civic.T4.Entities.Entity2();
                case "environment":
                case "Environment":
                    return new Civic.T4.Entities.Environment();
            };

            return null;
        }
    }

}


