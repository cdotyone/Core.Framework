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
using SAAS.Core.Framework;
using SAAS.Core.Framework.Test.Interfaces;

using ExampleEntity2 = SAAS.Core.Framework.Test.Entities.Entity2;

namespace SAAS.Core.Framework.Test.Data.SqlServer
{
    public partial class Entity2Repository : SqlRepository<ExampleEntity2>,IEntityRepository<ExampleEntity2>
	{
		public Entity2Repository(Container container) : base(container)
		{
		}
	}
}

