using System.Web.Http;

namespace Civic.Framework.WebApi.Test {

    public static class CivicWebApi
    {
        public static void Register(HttpConfiguration configuration)
        {
            Civic.Framework.WebApi.WebActivator.LoadControllers3(configuration);
        }
    }
}