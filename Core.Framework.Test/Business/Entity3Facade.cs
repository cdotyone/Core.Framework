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
using Core.Framework;

using ExampleEntity3 = Core.Framework.Test.Entities.Entity3;

namespace Core.Framework.Test.Business
{
    
	public partial class Entity3Facade : EntityBusinessFacade<ExampleEntity3>,IEntityBusinessFacade<ExampleEntity3>
	{
        public Entity3Facade(Container container, IEntityRepository<ExampleEntity3> repository, IEntityEventHandlerFactory handlers) : base(container, repository, handlers)
        {
        }
	}
}

