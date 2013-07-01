using System;
using System.Globalization;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PO.T4.WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ODataV3Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }
            var request = actionContext.Request;
            if (request == null)
            {
                //throw Error.Argument("actionExecutedContext", SRResources.ActionExecutedContextMustHaveRequest, new object[0]);
            }
            else
            {
                var raw = new ODataV3QueryOptions();

                var httpContext = request.Properties["MS_HttpContext"] as HttpContextBase;
                if (httpContext != null)
                {
                    var requestContext = httpContext.Request;

                    if (!string.IsNullOrEmpty(requestContext["$top"]))
                    {
                        int top;
                        int.TryParse(requestContext["$top"], NumberStyles.Integer, null, out top);
                        raw.Top = top;
                    }
                    if (!string.IsNullOrEmpty(requestContext["$skip"]))
                    {
                        int skip;
                        int.TryParse(requestContext["$skip"], NumberStyles.Integer, null, out skip);
                        raw.Skip = skip;
                    }

                    raw.InlineCount = string.Compare(requestContext["$inlinecount"], "allpages", StringComparison.InvariantCultureIgnoreCase) == 0;

                    raw.OrderBy = requestContext["$orderby"];
                    raw.Filter = requestContext["$filter"];
                    raw.Expand = requestContext["$expand"];
                    raw.Format = requestContext["$format"];
                }

                actionContext.ControllerContext.RouteData.Values.Add("options", raw);
            }


            base.OnActionExecuting(actionContext);
        }
    }
}