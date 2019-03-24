using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Core.Framework
{
    // Implement DefaultHttpControllerSelector, so we can override SelectController
    internal class VersionControllerSelector : DefaultHttpControllerSelector
    {
        public VersionControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // override the Accept header if a known odata $format is passed on the querystring
            var query = request.RequestUri.ParseQueryString();
            var format = query.GetValues("$format");
            if (format != null)
            {
                if (string.Compare(format[0], "xml", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(format[0], "json", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    if (request.Headers.Contains("Accept")) request.Headers.Remove("Accept");
                    request.Headers.Add("Accept", "application/" + format[0].ToLower());
                }
            }

            // Fall back to using the default implementation if no version is specified
            return base.SelectController(request);
        }
    }
}