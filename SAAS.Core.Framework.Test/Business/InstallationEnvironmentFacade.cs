﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is to supress no xml comments in public members warnings 

using SimpleInjector;
using SAAS.Core.Framework;

using ExampleInstallationEnvironment = SAAS.Core.Framework.Test.Entities.InstallationEnvironment;

namespace SAAS.Core.Framework.Test.Business
{
    
	public partial class InstallationEnvironmentFacade : EntityBusinessFacade<ExampleInstallationEnvironment>,IEntityBusinessFacade<ExampleInstallationEnvironment>
	{
        public InstallationEnvironmentFacade(Container container, IEntityRepository<ExampleInstallationEnvironment> repository, IEntityEventHandlerFactory handlers) : base(container, repository, handlers)
        {
        }
	}
}

