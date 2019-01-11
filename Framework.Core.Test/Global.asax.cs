using System;
using System.Web.Http;

namespace Civic.Framework.WebApi.Test
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ApiActivator.Initialize(GlobalConfiguration.Configuration);
        }
    }
}