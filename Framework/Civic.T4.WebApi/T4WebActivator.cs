using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.WebPages;
using Deco.Scheduler.Api.Views;
using Civic.Core.WebApi.Configuration;

namespace Civic.Core.WebApi
{
    public static class T4WebActivator
    {
        private static readonly List<Assembly> _assemblies = new List<Assembly>();

        public static void LoadControllers()
        {
            var assemblyNames = new List<string>();

            if (T4WebApiSection.Current == null || T4WebApiSection.Current.Assemblies == null || T4WebApiSection.Current.Assemblies.Count == 0)
            {
                var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                if (directoryName != null)
                {
                    var binFolder = directoryName.Replace("file:\\", "");
                    var dir = new DirectoryInfo(binFolder);
                    var files = dir.GetFiles("*.dll");

                    foreach (var file in files)
                    {
                        var dllname = file.Name.Substring(0, file.Name.Length - file.Extension.Length);
                        if (!assemblyNames.Contains(dllname)) assemblyNames.Add(dllname);
                    }
                }
            }
            else
            {
                foreach (var assemblyElement in T4WebApiSection.Current.Assemblies)
                {
                    if (!assemblyNames.Contains(assemblyElement.Key)) assemblyNames.Add(assemblyElement.Key);
                }
            }

            var dlls = new List<IEntityRouteManager>();
            var itype = typeof (IEntityRouteManager);
            foreach (var dllname in assemblyNames)
            {
                var asm = Assembly.Load(dllname);

                foreach (var type in asm.GetTypes())
                {
                    if (type.GetInterface(itype.Name) != null)
                    {
                        _assemblies.Add(asm);
                        dlls.Add((IEntityRouteManager) Activator.CreateInstance(type));
                    }
                }
            }

            var versionSelector = new VersionControllerSelector(GlobalConfiguration.Configuration);
            foreach (IEntityRouteManager entityRouteManager in dlls)
            {
                versionSelector.AddUnknownRoutes(entityRouteManager.GetType().Assembly);
                versionSelector.AddKnownRoutes(entityRouteManager.GetEntityRoutes());
            }

            GlobalConfiguration.Configuration.Services.Replace(typeof (IHttpControllerSelector), versionSelector);

            GlobalConfiguration.Configuration.EnableODataV3Support();
        }

        public static void LoadViews()
        {
            foreach (var assembly in _assemblies)
            {
                var engine = new PrecompiledMvcEngine(assembly)
                {
                    UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
                };

                ViewEngines.Engines.Insert(0, engine);

                // StartPage lookups are done by WebPages. 
                VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
            }            
        }
    }
}
