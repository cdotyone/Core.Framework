﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using Core.Logging;

namespace Core.Framework
{
    public static class ApiActivator
    {
  
        public static Container Container {  get; private set; } = new Container();

        public static void ConfigureServices(IServiceCollection services)
        {
            var container = Container;

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddHttpContextAccessor();

//#if NETFULL
//            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));
//            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(container));
//#endif

#if NETCORE
            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation()
                    .AddViewComponentActivation();
            });
#endif

            //services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (Container == null) Container = new Container();
            var container = Container;

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            try
            {
                var fileName = Path.Combine(env.WebRootPath, "entityConfig.json");
                EntityInfoManager.Configuration.Load(fileName);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(LoggingBoundaries.Host, ex);
            }


            // Register your types, for instance using the scoped lifestyle:
            try
            {
                var path = AppDomain.CurrentDomain.DynamicDirectory;
                if (string.IsNullOrEmpty(path)) path = AppDomain.CurrentDomain.BaseDirectory;

                var assemblies = new DirectoryInfo(path).GetFiles("*.dll", SearchOption.AllDirectories);
                var valid = assemblies.Where(file => !file.Name.Contains("System.Net.Http.Extensions"));
                var list = new List<Assembly>();
                foreach (var file in valid)
                {
                    try
                    {
                        var assembly = Assembly.Load(AssemblyName.GetAssemblyName(file.FullName));
                        list.Add(assembly);
                    }
                    catch (Exception ex)
                    {
                        Logger.HandleException(LoggingBoundaries.Host, ex);
                    }
                }
                container.RegisterPackages(list);
            }
            catch (Exception ex)
            {
                Logger.HandleException(LoggingBoundaries.Host, ex);
            }


            //var versionSelector = new VersionControllerSelector(configuration);

            //configuration.Services.Replace(typeof(IHttpControllerSelector), versionSelector);
            //configuration.EnableODataV3Support();

#if NETFULL
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
#endif

            
            //            container.RegisterMvcViewComponents(app);
            //            container.AutoCrossWireAspNetComponents(app);
            container.Verify();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultGet",
                    template: "api/{module}/{version}/{entity}/{key}",
                    //constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
                    defaults: new { controller = "Services", action = "DefaultGet" }
                    );

                routes.MapRoute(
                    name: "DefaultGet",
                    template: "api/{module}/{version}/{entity}/{key}",
                    //constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
                    defaults: new { controller = "Services", action = "DefaultGet" }
                );
                routes.MapRoute(
                    name: "DefaultDelete",
                    template: "api/{module}/{version}/{entity}/{key}",
                    //constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) },
                    defaults: new { controller = "Services", action = "DefaultDelete" }
                );
                routes.MapRoute(
                    name: "DefaultPut",
                    template: "api/{module}/{version}/{entity}/{key}",
                    //constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
                    defaults: new { controller = "Services", action = "DefaultPut" }
                );
                routes.MapRoute(
                    name: "DefaultPost",
                    template: "api/{module}/{version}/{entity}",
                    //constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
                    defaults: new { controller = "Services", action = "DefaultPost" }
                );
                routes.MapRoute(
                    name: "DefaultPostBulk",
                    template: "api/services",
                    //constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
                    defaults: new { controller = "Services", action = "DefaultPostBulk" }
                );

            });

            /*

                        if (Core.Logging.Configuration.LoggingConfig.Current.Transmission)
                        {
                            Logger.LogTrace(LoggingBoundaries.Host, "Transmission logging on");
                            configuration.MessageHandlers.Add(new TransmissionLogHandler());
                        }
                        else Logger.LogTrace(LoggingBoundaries.Host, "Transmission logging off");*/

            try
            {
                var fileName = Path.Combine(env.WebRootPath, "entityConfig.json");
                EntityInfoManager.Configuration.Save(fileName);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(LoggingBoundaries.Host, ex);
            }
        }

    }
}
