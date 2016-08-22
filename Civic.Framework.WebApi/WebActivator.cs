using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Civic.Framework.WebApi
{
    public static class WebActivator
    {
        public static void LoadControllers()
        {
            var versionSelector = new VersionControllerSelector(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.Services.Replace(typeof (IHttpControllerSelector), versionSelector);
            GlobalConfiguration.Configuration.EnableODataV3Support();
        }

        public static void LoadViews()
        {
        }
    }
}
