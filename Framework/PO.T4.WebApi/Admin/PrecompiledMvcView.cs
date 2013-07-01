﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Deco.Scheduler.Api.Views
{
    public class PrecompiledMvcView : IView
    {
        private static Action<WebViewPage, string> _overriddenLayoutSetter;
        private readonly Type _type;
        private readonly string _virtualPath;
        private readonly string _masterPath;


        public PrecompiledMvcView(string virtualPath, Type type, bool runViewStartPages, IEnumerable<string> fileExtension)
            : this(virtualPath, null, type, runViewStartPages, fileExtension)
        {
        }

        public PrecompiledMvcView(string virtualPath, string masterPath, Type type, bool runViewStartPages, IEnumerable<string> fileExtension)
        {
            _type = type;
            _virtualPath = virtualPath;
            _masterPath = masterPath;
            RunViewStartPages = runViewStartPages;
            ViewStartFileExtensions = fileExtension;
        }

        public bool RunViewStartPages
        {
            get;
            private set;
        }

        public IEnumerable<string> ViewStartFileExtensions
        {
            get;
            private set;
        }

        public string VirtualPath
        {
            get { return _virtualPath; }
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            object instance = null;
            if (DependencyResolver.Current != null)
            {
                var viewPageActivator = DependencyResolver.Current.GetService<IViewPageActivator>();
                if (viewPageActivator != null)
                    instance = viewPageActivator.Create(viewContext.Controller.ControllerContext, _type);
                else
                    instance = DependencyResolver.Current.GetService(_type);
            }
            if (instance == null)
                instance = Activator.CreateInstance(_type);

            WebViewPage webViewPage = instance as WebViewPage;

            if (webViewPage == null)
            {
                throw new InvalidOperationException("Invalid view type");
            }

            if (!String.IsNullOrEmpty(_masterPath))
            {
                EnsureLayoutSetter();
                _overriddenLayoutSetter(webViewPage, _masterPath);
            }

            webViewPage.VirtualPath = _virtualPath;
            webViewPage.ViewContext = viewContext;
            webViewPage.ViewData = viewContext.ViewData;
            webViewPage.InitHelpers();

            WebPageRenderingBase startPage = null;
            if (this.RunViewStartPages)
            {
                startPage = StartPage.GetStartPage(webViewPage, "_ViewStart", ViewStartFileExtensions);
            }

            var pageContext = new WebPageContext(viewContext.HttpContext, webViewPage, null);
            webViewPage.ExecutePageHierarchy(pageContext, writer, startPage);
        }

        /// <summary>
        /// Ensures that the static method CreateOverriddenLayoutSetterDelegate() has 
        /// been set. This allows for the class to be instantiated without setting the 
        /// static ctr and causing medium trust exceptions (reflection). 
        /// However, if the precompiled view uses a layout page then it will be called anyway
        /// Thus, medium trust != layout
        /// </summary>
        private void EnsureLayoutSetter()
        {
            if (_overriddenLayoutSetter == null)
            {
                _overriddenLayoutSetter = CreateOverriddenLayoutSetterDelegate();
            }
        }

        // Unfortunately, the only way to override the default layout with a custom layout from a
        // ViewResult, without introducing a new subclass, is by setting the WebViewPage internal
        // property OverridenLayoutPath [sic].
        // This method makes use of reflection for creating a property setter in the form of a
        // delegate. The latter is used to improve performance, compared to invoking the MethodInfo
        // instance directly, without sacrificing maintainability.
        private static Action<WebViewPage, string> CreateOverriddenLayoutSetterDelegate()
        {
            PropertyInfo property = typeof(WebViewPage).GetProperty("OverridenLayoutPath",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (property == null)
            {
                throw new NotSupportedException("The WebViewPage internal property \"OverridenLayoutPath\" does not exist, probably due to an unsupported run-time version.");
            }

            MethodInfo setter = property.GetSetMethod(nonPublic: true);
            if (setter == null)
            {
                throw new NotSupportedException("The WebViewPage internal property \"OverridenLayoutPath\" exists but is missing a set method, probably due to an unsupported run-time version.");
            }

            return (Action<WebViewPage, string>)Delegate.CreateDelegate(typeof(Action<WebViewPage, string>), setter, throwOnBindFailure: true);
        }
    }
}
