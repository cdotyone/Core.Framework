using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Core.Logging;

namespace Core.Framework.OData
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ODataV3Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext));
            }
            var request = actionContext.Request;
            if (request != null)
            {
                var raw = new ODataV3QueryOptions();

                if (request.Properties.ContainsKey("MS_QueryNameValuePairs"))
                {
                    try
                    {
                        var query = (KeyValuePair<string, string>[])request.Properties["MS_QueryNameValuePairs"];

                        foreach (var pair in query)
                        {
                            var name = pair.Key.ToLowerInvariant();
                            switch (name)
                            {
                                case "$top":
                                    int.TryParse(pair.Value, NumberStyles.Integer, null, out var top);
                                    raw.Top = top;
                                    break;
                                case "$skip":
                                    int.TryParse(pair.Value, NumberStyles.Integer, null, out var skip);
                                    raw.Skip = skip;
                                    break;
                                case "$inlinecount":
                                    raw.InlineCount = string.Compare(pair.Value, "allpages", StringComparison.InvariantCultureIgnoreCase) == 0;
                                    break;
                                case "$orderby":
                                    raw.OrderBy = pair.Value;
                                    break;
                                case "$filter":
                                    raw.Filter = pair.Value;
                                    break;
                                case "$expand":
                                    raw.Expand = pair.Value;
                                    break;
                                case "$format":
                                    raw.Format = pair.Value;
                                    break;
                            }
                        }

                        actionContext.ControllerContext.RouteData.Values.Add("options", raw);
                    }
                    catch (Exception ex)
                    {
                        Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex);
                    }
                }

                else 

                if (request.Properties.ContainsKey("MS_HttpContext"))
                {
                    if (request.Properties["MS_HttpContext"] is HttpContextBase httpContext)
                    {
                        var requestContext = httpContext.Request;

                        if (!string.IsNullOrEmpty(requestContext["$top"]))
                        {
                            int.TryParse(requestContext["$top"], NumberStyles.Integer, null, out var top);
                            raw.Top = top;
                        }

                        if (!string.IsNullOrEmpty(requestContext["$skip"]))
                        {
                            int.TryParse(requestContext["$skip"], NumberStyles.Integer, null, out int skip);
                            raw.Skip = skip;
                        }

                        raw.InlineCount = string.Compare(requestContext["$inlinecount"], "allpages",
                                              StringComparison.InvariantCultureIgnoreCase) == 0;

                        raw.OrderBy = requestContext["$orderby"];
                        raw.Filter = requestContext["$filter"];
                        raw.Expand = requestContext["$expand"];
                        raw.Format = requestContext["$format"];

                        actionContext.ControllerContext.RouteData.Values.Add("options", raw);
                    }
                }                 
            }

            base.OnActionExecuting(actionContext);
        }
    }
}