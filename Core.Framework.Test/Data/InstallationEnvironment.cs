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
using Core.Framework;
using Core.Framework.Test.Interfaces;

using ExampleInstallationEnvironment = Core.Framework.Test.Entities.InstallationEnvironment;

namespace Core.Framework.Test.Data.SqlServer
{
    public partial class InstallationEnvironmentRepository : SqlRepository<ExampleInstallationEnvironment>,IEntityRepository<ExampleInstallationEnvironment>
	{
		public InstallationEnvironmentRepository(Container container) : base(container)
		{
		}
	}
}

