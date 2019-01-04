﻿
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is to supress no xml comments in public members warnings 

using SimpleInjector.Packaging;
using SimpleInjector;



namespace Civic.Framework.WebApi.Test.Business
{

    public class ExampleFacadePackage : IPackage
    {
        public void RegisterServices(Container container)
        {
    		var factory = new EntityCreateFactory(container);
    
    		factory.Register<Civic.Framework.WebApi.Test.Entities.Entity1>(Civic.Framework.WebApi.Test.Entities.Entity1.Info);
    		factory.Register<Civic.Framework.WebApi.Test.Entities.Entity2>(Civic.Framework.WebApi.Test.Entities.Entity2.Info);
    		factory.Register<Civic.Framework.WebApi.Test.Entities.Entity3>(Civic.Framework.WebApi.Test.Entities.Entity3.Info);
    		factory.Register<Civic.Framework.WebApi.Test.Entities.Environment>(Civic.Framework.WebApi.Test.Entities.Environment.Info);
    		factory.Register<Civic.Framework.WebApi.Test.Entities.InstallationEnvironment>(Civic.Framework.WebApi.Test.Entities.InstallationEnvironment.Info);
    		container.Register<Civic.Framework.WebApi.Test.Interfaces.IEntity1Facade, Entity1Facade>(Lifestyle.Singleton);
    		container.Register<Civic.Framework.WebApi.Test.Interfaces.IEntity2Facade, Entity2Facade>(Lifestyle.Singleton);
    		container.Register<Civic.Framework.WebApi.Test.Interfaces.IEntity3Facade, Entity3Facade>(Lifestyle.Singleton);
    		container.Register<Civic.Framework.WebApi.Test.Interfaces.IEnvironmentFacade, EnvironmentFacade>(Lifestyle.Singleton);
    		container.Register<Civic.Framework.WebApi.Test.Interfaces.IInstallationEnvironmentFacade, InstallationEnvironmentFacade>(Lifestyle.Singleton);
    
        }
    }
    

}


