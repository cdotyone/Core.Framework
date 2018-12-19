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
        public static void LoadControllers()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            var path = AppDomain.CurrentDomain.DynamicDirectory; ;
            var assemblies = from file in new DirectoryInfo(path).GetFiles("*.dll", SearchOption.AllDirectories)
                select Assembly.Load(AssemblyName.GetAssemblyName(file.FullName));
            var alist = assemblies.ToList();
            container.RegisterPackages(alist);

            var versionSelector = new VersionControllerSelector(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.Services.Replace(typeof (IHttpControllerSelector), versionSelector);
            GlobalConfiguration.Configuration.EnableODataV3Support();

            if (Civic.Core.Logging.Configuration.LoggingConfig.Current.Transmission)
            {
                Logger.LogTrace(LoggingBoundaries.Host, "Transmission logging on");
                GlobalConfiguration.Configuration.MessageHandlers.Add(new TransmissionLogHandler());
            }
            else Logger.LogTrace(LoggingBoundaries.Host, "Transmission logging off");

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration, alist);
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        public static void LoadViews()
        {
        }
    }
}
