using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Filters;

namespace PO.T4.WebApi
{
    public static class ODataV3FormatterExtensions
    {
        public static ODataV3QueryOptions GetOptions(this ApiController controller)
        {
            return controller.ControllerContext.RouteData.Values["options"] as ODataV3QueryOptions ??
                                           new ODataV3QueryOptions();
        }

        public static void EnableODataV3Support(this HttpConfiguration configuration)
        {
            int xpos = -1, jpos = -1, pos = 0;
            foreach (var formatter in configuration.Formatters)
            {
                if (formatter is JsonMediaTypeFormatter) jpos = pos;
                if (formatter is XmlMediaTypeFormatter) xpos = pos;
                pos++;
            }
            if (xpos != -1) configuration.Formatters.RemoveAt(xpos);
            if (jpos != -1) configuration.Formatters.RemoveAt(jpos);

            configuration.Formatters.Add(new ODataV3JsonFormatter());
            configuration.Formatters.Add(new ODataV3XmlFormatter());

            configuration.Services.Add(typeof(IFilterProvider), new ODataV3QueryFilter());
        }

        public static string ProcessOrderByOptions(this ODataV3QueryOptions rawOptions)
        {
            string orderby = null;
            if (!string.IsNullOrEmpty(rawOptions.OrderBy))
            {
                orderby = rawOptions.OrderBy.ToUpper() + ",";
                orderby = orderby.Replace(" ,", ",");
                orderby = orderby.Replace(" ASC,", "_ASC");
                orderby = orderby.Replace(" DESC,", "_DESC,");
                orderby = orderby.Trim(new[] {' ', ','});

                var parts = orderby.Split(',');
                var list = new List<string>();
                foreach (var part in parts)
                {
                    if (!(part.EndsWith("_ASC") || part.EndsWith("_DESC")))
                        list.Add(part + "_ASC");
                    else list.Add(part);
                }
                orderby = string.Join(",", list.ToArray());
            }
            return orderby;
        }
    }
}