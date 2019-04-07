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
using Core.Framework;
using Core.Framework.Test.Entities;

namespace Core.Framework.Test.Data.SqlServer
{
    

public class ExampleRepositoryPackage : IPackage
{
    public void RegisterServices(Container container)
    {

		container.Register<IEntityRepository<Entity1>, Entity1Repository>(Lifestyle.Singleton);

		container.Register<IEntityRepository<Entity2>, Entity2Repository>(Lifestyle.Singleton);

		container.Register<IEntityRepository<Entity3>, Entity3Repository>(Lifestyle.Singleton);

		container.Register<IEntityRepository<Environment>, EnvironmentRepository>(Lifestyle.Singleton);

		container.Register<IEntityRepository<InstallationEnvironment>, InstallationEnvironmentRepository>(Lifestyle.Singleton);

    }
}

}
