using System;
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

            // Register your types, for instance using the scoped lifestyle:
            var path = AppDomain.CurrentDomain.DynamicDirectory; ;
            var assemblies = from file in new DirectoryInfo(path).GetFiles("*.dll", SearchOption.AllDirectories)
                select Assembly.Load(AssemblyName.GetAssemblyName(file.FullName));
            var alist = assemblies.ToList();
            container.RegisterPackages(alist);

            var versionSelector = new VersionControllerSelector(configuration);

            configuration.Services.Replace(typeof(IHttpControllerSelector), versionSelector);
            configuration.EnableODataV3Support();

            configuration.Routes.MapHttpRoute(
                name: "DefaultGetPaged",
                routeTemplate: "api/{module}/{version}/{entity}",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
                defaults: new {  controller="Services", action = "GetPaged" }
            );
            configuration.Routes.MapHttpRoute(
                name: "DefaultGet",
                routeTemplate: "api/{module}/{version}/{entity}/{key}",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
                defaults: new { controller = "Services", action = "Get" }
            );
            configuration.Routes.MapHttpRoute(
                name: "DefaultDelete",
                routeTemplate: "api/{module}/{version}/{entity}/{key}",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) },
                defaults: new { controller = "Services", action = "Remove" }
            );
            configuration.Routes.MapHttpRoute(
                name: "DefaultPut",
                routeTemplate: "api/{module}/{version}/{entity}/{key}",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
                defaults: new { controller = "Services", action = "Put" }
            );
            configuration.Routes.MapHttpRoute(
                name: "DefaultPost",
                routeTemplate: "api/{module}/{version}/{entity}",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
                defaults: new { controller = "Services", action = "Post" }
            );
            configuration.Routes.MapHttpRoute(
                name: "DefaultPostBulk",
                routeTemplate: "api/services",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
                defaults: new { controller = "Services", action = "PostBulk" }
            );


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
    }
}
