﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is to supress no xml comments in public members warnings 

using System.Runtime.InteropServices;
using SimpleInjector.Packaging;
using SimpleInjector;

namespace SAAS.Core.Framework
{
    public class WebApiPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IEntityCreateFactory, EntityCreateFactory>(Lifestyle.Singleton);
            container.Register<IEntityEventHandlerFactory, EntityEventHandlerFactory>(Lifestyle.Singleton);

            var factory = new EntityEventHandlerFactory();
            factory.Register(new BasicAuthorizationHandler());
            factory.Register(new AuditLogHandler());
        }
    }
    
}


