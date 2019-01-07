using System.Web.Http;
using System.Web.Http.Dispatcher;
using Civic.Core.Logging;
using Civic.Framework.WebApi.Logging;

namespace Civic.Framework.WebApi
{
    public static class WebActivator
    {
        public static void LoadControllers()
        {
            var versionSelector = new VersionControllerSelector(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), versionSelector);
            GlobalConfiguration.Configuration.EnableODataV3Support();

            if (Civic.Core.Logging.Configuration.LoggingConfig.Current.Transmission)
            {
                Logger.LogTrace(LoggingBoundaries.Host, "Transmission logging on");
                GlobalConfiguration.Configuration.MessageHandlers.Add(new TransmissionLogHandler());
            }
            else Logger.LogTrace(LoggingBoundaries.Host, "Transmission logging off");
        }

        public static void LoadViews()
        {
        }
    }
}
