﻿//------------------------------------------------------------------------------
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



namespace Core.Framework.Test.Business
{
    

public class ExampleFacadePackage : IPackage
{
    public void RegisterServices(Container container)
    {
		var factory = new EntityCreateFactory(container);

		factory.Register<Core.Framework.Test.Entities.Entity1, Entity1Facade>();
		factory.Register<Core.Framework.Test.Entities.Entity2, Entity2Facade>();
		factory.Register<Core.Framework.Test.Entities.Entity3, Entity3Facade>();
		factory.Register<Core.Framework.Test.Entities.Environment, EnvironmentFacade>();
		factory.Register<Core.Framework.Test.Entities.InstallationEnvironment, InstallationEnvironmentFacade>();


    }
}

}



