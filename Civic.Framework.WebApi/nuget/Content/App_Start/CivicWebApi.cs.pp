using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.CivicWebApi), "PreStart")]
namespace $rootnamespace$.App_Start {

    public static class CivicWebApi
    {
        public static void PreStart()
        {
            Civic.Framework.WebApi.WebActivator.LoadControllers();
        }
    }
}