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
using Framework.Core;



namespace Framework.Core.Test.Data.SqlServer
{
    

public class ExampleRepositoryPackage : IPackage
{
    public void RegisterServices(Container container)
    {

		container.Register<IEntityRepository<Framework.Core.Test.Interfaces.IEntity1>, Entity1Repository>(Lifestyle.Singleton);

		container.Register<IEntityRepository<Framework.Core.Test.Interfaces.IEntity2>, Entity2Repository>(Lifestyle.Singleton);

		container.Register<IEntityRepository<Framework.Core.Test.Interfaces.IEntity3>, Entity3Repository>(Lifestyle.Singleton);

		container.Register<IEntityRepository<Framework.Core.Test.Interfaces.IEnvironment>, EnvironmentRepository>(Lifestyle.Singleton);

		container.Register<IEntityRepository<Framework.Core.Test.Interfaces.IInstallationEnvironment>, InstallationEnvironmentRepository>(Lifestyle.Singleton);

    }
}

}

