using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Deco.Scheduler.Api.Views
{
    public class PrecompiledMvcEngine : VirtualPathProviderViewEngine, IVirtualPathFactory
    {
        private readonly IDictionary<string, Type> _mappings;
        private readonly string _baseVirtualPath;
        private readonly Lazy<DateTime> _assemblyLastWriteTime;

        public bool PreemptPhysicalFiles { get; set; }

        public bool UsePhysicalViewsIfNewer { get; set; }

        public PrecompiledMvcEngine(Assembly assembly)
            : this(assembly, (string) null)
        {
        }

        public PrecompiledMvcEngine(Assembly assembly, string baseVirtualPath)
        {
            PrecompiledMvcEngine precompiledMvcEngine = this;
            this._assemblyLastWriteTime =
                new Lazy<DateTime>(
                    (Func<DateTime>) (() => AssemblyExtensions.GetLastWriteTimeUtc(assembly, DateTime.MaxValue)));
            this._baseVirtualPath = PrecompiledMvcEngine.NormalizeBaseVirtualPath(baseVirtualPath);
            this.AreaViewLocationFormats = new string[2]
                {
                    "~/Areas/{2}/Views/{1}/{0}.cshtml",
                    "~/Areas/{2}/Views/Shared/{0}.cshtml"
                };
            this.AreaMasterLocationFormats = new string[2]
                {
                    "~/Areas/{2}/Views/{1}/{0}.cshtml",
                    "~/Areas/{2}/Views/Shared/{0}.cshtml"
                };
            this.AreaPartialViewLocationFormats = new string[2]
                {
                    "~/Areas/{2}/Views/{1}/{0}.cshtml",
                    "~/Areas/{2}/Views/Shared/{0}.cshtml"
                };
            this.ViewLocationFormats = new string[2]
                {
                    "~/Views/{1}/{0}.cshtml",
                    "~/Views/Shared/{0}.cshtml"
                };
            this.MasterLocationFormats = new string[2]
                {
                    "~/Views/{1}/{0}.cshtml",
                    "~/Views/Shared/{0}.cshtml"
                };
            this.PartialViewLocationFormats = new string[2]
                {
                    "~/Views/{1}/{0}.cshtml",
                    "~/Views/Shared/{0}.cshtml"
                };
            this.FileExtensions = new string[1]
                {
                    "cshtml"
                };
            this._mappings =
                (IDictionary<string, Type>)
                Enumerable.ToDictionary<KeyValuePair<string, Type>, string, Type>(
                    Enumerable.Select(
                        Enumerable.Where(
                            Enumerable.Select(
                                Enumerable.Where<Type>((IEnumerable<Type>) assembly.GetTypes(),
                                                       (Func<Type, bool>)
                                                       (type => typeof (WebPageRenderingBase).IsAssignableFrom(type))),
                                type =>
                                    {
                                        var local_0 = new
                                            {
                                                type = type,
                                                pageVirtualPath =
                                                    Enumerable.FirstOrDefault<PageVirtualPathAttribute>(
                                                        Enumerable.OfType<PageVirtualPathAttribute>(
                                                            (IEnumerable) type.GetCustomAttributes(false)))
                                            };
                                        return local_0;
                                    }), param0 => param0.pageVirtualPath != null),
                        param0 =>
                        new KeyValuePair<string, Type>(
                            PrecompiledMvcEngine.CombineVirtualPaths(this._baseVirtualPath,
                                                                     param0.pageVirtualPath.VirtualPath), param0.type)),
                    (Func<KeyValuePair<string, Type>, string>) (t => t.Key),
                    (Func<KeyValuePair<string, Type>, Type>) (t => t.Value),
                    (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
            this.ViewLocationCache =
                (IViewLocationCache) new PrecompiledViewLocationCache(assembly.FullName, this.ViewLocationCache);
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            if (this.UsePhysicalViewsIfNewer && this.IsPhysicalFileNewer(virtualPath))
                return false;
            else
                return this.Exists(virtualPath);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return this.CreateViewInternal(partialPath, (string) null, false);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return this.CreateViewInternal(viewPath, masterPath, true);
        }

        private IView CreateViewInternal(string viewPath, string masterPath, bool runViewStartPages)
        {
            Type type;
            if (this._mappings.TryGetValue(viewPath, out type))
                return
                    (IView)
                    new PrecompiledMvcView(viewPath, masterPath, type, runViewStartPages,
                                           (IEnumerable<string>) this.FileExtensions);
            else
                return (IView) null;
        }

        public object CreateInstance(string virtualPath)
        {
            if (!this.PreemptPhysicalFiles && this.VirtualPathProvider.FileExists(virtualPath))
                return BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof (WebPageRenderingBase));
            if (this.UsePhysicalViewsIfNewer && this.IsPhysicalFileNewer(virtualPath))
                return BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof (WebViewPage));
            Type type;
            if (!this._mappings.TryGetValue(virtualPath, out type))
                return (object) null;
            if (DependencyResolver.Current != null)
                return DependencyResolver.Current.GetService(type);
            else
                return Activator.CreateInstance(type);
        }

        public bool Exists(string virtualPath)
        {
            return this._mappings.ContainsKey(virtualPath);
        }

        private bool IsPhysicalFileNewer(string virtualPath)
        {
            if (!virtualPath.StartsWith(this._baseVirtualPath ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                return false;
            if (!string.IsNullOrEmpty(this._baseVirtualPath))
                virtualPath = (string) (object) '~' + (object) virtualPath.Substring(this._baseVirtualPath.Length);
            string path = HttpContext.Current.Request.MapPath(virtualPath);
            return File.Exists(path) && File.GetLastWriteTimeUtc(path) > this._assemblyLastWriteTime.Value;
        }

        private static string NormalizeBaseVirtualPath(string virtualPath)
        {
            if (!string.IsNullOrEmpty(virtualPath))
            {
                if (!virtualPath.StartsWith("~/", StringComparison.Ordinal))
                    virtualPath = "~/" + virtualPath;
                if (!virtualPath.EndsWith("/", StringComparison.Ordinal))
                    virtualPath = virtualPath + "/";
            }
            return virtualPath;
        }

        private static string CombineVirtualPaths(string baseVirtualPath, string virtualPath)
        {
            if (!string.IsNullOrEmpty(baseVirtualPath))
                return VirtualPathUtility.Combine(baseVirtualPath, virtualPath.Substring(2));
            else
                return virtualPath;
        }
    }
}

