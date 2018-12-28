using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Civic.Core.Logging;
using Civic.Framework.WebApi.Logging;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace Civic.Framework.WebApi
{
    public static class WebActivator
    {
        [Obsolete("Please use LoadControllers2 or LoadControllers3 based on your generation revision")]
        public static void LoadControllers()
        {
            LoadControllers2(GlobalConfiguration.Configuration);
        }

        public static void LoadControllers2(HttpConfiguration configuration)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            var path = AppDomain.CurrentDomain.DynamicDirectory; ;
            var assemblies = from file in new DirectoryInfo(path).GetFiles("*.dll", SearchOption.AllDirectories)
                select Assembly.Load(AssemblyName.GetAssemblyName(file.FullName));
            var alist = assemblies.ToList();
            container.RegisterPackages(alist);

            var versionSelector = new VersionControllerSelector(configuration);

            configuration.Services.Replace(typeof(IHttpControllerSelector), versionSelector);
            configuration.EnableODataV3Support();

            if (Civic.Core.Logging.Configuration.LoggingConfig.Current.Transmission)
            {
                Logger.LogTrace(LoggingBoundaries.Host, "Transmission logging on");
                configuration.MessageHandlers.Add(new TransmissionLogHandler());
            }
            else Logger.LogTrace(LoggingBoundaries.Host, "Transmission logging off");

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(configuration, alist);
            container.Verify();

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        public static void LoadControllers3(HttpConfiguration configuration)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            var path = AppDomain.CurrentDomain.DynamicDirectory; ;
            var assemblies = from file in new DirectoryInfo(path).GetFiles("*.dll", SearchOption.AllDirectories)
                select Assembly.Load(AssemblyName.GetAssemblyName(file.FullName));
            var alist = assemblies.ToList();
            container.RegisterPackages(alist);

            var versionSelector = new VersionControllerSelector(configuration);

            configuration.Services.Replace(typeof (IHttpControllerSelector), versionSelector);
            configuration.EnableODataV3Support();

            if (Civic.Core.Logging.Configuration.LoggingConfig.Current.Transmission)
            {
                Logger.LogTrace(LoggingBoundaries.Host, "Transmission logging on");
                configuration.MessageHandlers.Add(new TransmissionLogHandler());
            }
            else Logger.LogTrace(LoggingBoundaries.Host, "Transmission logging off");

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(configuration, alist);
            container.Verify();

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        public static void LoadViews()
        {
        }
    }
}
