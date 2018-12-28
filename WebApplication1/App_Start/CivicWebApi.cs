using System.Web.Http;
using Civic.Framework.WebApi.Test;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CivicWebApi), "PreStart")]
namespace Civic.Framework.WebApi.Test {

    public static class CivicWebApi
    {
        public static void PreStart(HttpConfiguration configuration)
        {
            Civic.Framework.WebApi.WebActivator.LoadControllers3(configuration);
        }
    }
}