using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using Civic.Core.Logging;
using SAAS.Core.Framework.Logging;
using SAAS.Core.Framework.OData;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace SAAS.Core.Framework
{
    public class ApiActivator
    {
  
        public static Container Container {  get; private set; }

        public static HttpConfiguration Configuration { get; private set; }

        public static void Initialize(HttpConfiguration configuration)
        {
            if (Container == null) Container = new Container();
            var container = Container;

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            try
            {
                var fileName = System.Web.Hosting.HostingEnvironment.MapPath("~/entityConfig.json");
                EntityInfoManager.Configuration.Load(fileName);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(LoggingBoundaries.Host, ex);
            }


            // Register your types, for instance using the scoped lifestyle:
            List<Assembly> alist = null;
            try
            {
                var path = AppDomain.CurrentDomain.DynamicDirectory;
                var assemblies = new DirectoryInfo(path).GetFiles("*.dll", SearchOption.AllDirectories)
                    .Where(file => !file.Name.Contains("System.Net.Http.Extensions"))
                    .Select(file => Assembly.Load(AssemblyName.GetAssemblyName(file.FullName)));
                alist = assemblies.ToList();
                container.RegisterPackages(alist);
            }
            catch (Exception ex)
            {
                Logger.HandleException(LoggingBoundaries.Host, ex);
            }


            var versionSelector = new VersionControllerSelector(configuration);

            configuration.Services.Replace(typeof(IHttpControllerSelector), versionSelector);
            configuration.EnableODataV3Support();

            configuration.Routes.MapHttpRoute(
                name: "DefaultGetPaged",
                routeTemplate: "api/{module}/{version}/{entity}",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
                defaults: new {  controller="Services", action = "DefaultGetPaged" }
            );
            configuration.Routes.MapHttpRoute(
                name: "DefaultGet",
                routeTemplate: "api/{module}/{version}/{entity}/{key}",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
                defaults: new { controller = "Services", action = "DefaultGet" }
            );
            configuration.Routes.MapHttpRoute(
                name: "DefaultDelete",
                routeTemplate: "api/{module}/{version}/{entity}/{key}",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) },
                defaults: new { controller = "Services", action = "DefaultDelete" }
            );
            configuration.Routes.MapHttpRoute(
                name: "DefaultPut",
                routeTemplate: "api/{module}/{version}/{entity}/{key}",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
                defaults: new { controller = "Services", action = "DefaultPut" }
            );
            configuration.Routes.MapHttpRoute(
                name: "DefaultPost",
                routeTemplate: "api/{module}/{version}/{entity}",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
                defaults: new { controller = "Services", action = "DefaultPost" }
            );
            configuration.Routes.MapHttpRoute(
                name: "DefaultPostBulk",
                routeTemplate: "api/services",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
                defaults: new { controller = "Services", action = "DefaultPostBulk" }
            );


            if (Civic.Core.Logging.Configuration.LoggingConfig.Current.Transmission)
            {
                Logger.LogTrace(LoggingBoundaries.Host, "Transmission logging on");
                configuration.MessageHandlers.Add(new TransmissionLogHandler());
            }
            else Logger.LogTrace(LoggingBoundaries.Host, "Transmission logging off");

            // This is an extension method from the integration package.
            if(alist!=null) container.RegisterWebApiControllers(configuration, alist);
            container.Verify();

            try
            {
                var fileName = System.Web.Hosting.HostingEnvironment.MapPath("~/entityConfig.json");
                EntityInfoManager.Configuration.Save(fileName);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(LoggingBoundaries.Host, ex);
            }

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

        }
    }
}
