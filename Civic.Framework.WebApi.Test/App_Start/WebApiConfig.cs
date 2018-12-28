using System.Web.Http;

namespace Civic.Framework.WebApi.Test
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }
}
