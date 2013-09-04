using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Civic.T4.WebApi;

[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.T4WebApi), "PreStart")]
namespace $rootnamespace$.App_Start {

    public static class T4WebApi
    {
        public static void PreStart()
        {
            T4WebActivator.LoadControllers();
        }
    }
}