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
using SAAS.Core.Framework.Test.Interfaces;



using IExampleEntity1 = SAAS.Core.Framework.Test.Interfaces.IEntity1;
namespace SAAS.Core.Framework.Test.Business
{
    

public partial class Entity1Facade : EntityBusinessFacade<IExampleEntity1>, IEntity1Facade
{

        public Entity1Facade(Container container, IEntityRepository<IExampleEntity1> repository, IEntityEventHandlerFactory handlers) : base(container, repository, handlers)
        {
        }

}
}
