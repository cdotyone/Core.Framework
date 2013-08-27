using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Civic.Core.WebApi
{
    public class ODataV3QueryFilter : IFilterProvider
    {
        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            return new FilterInfo[]
            {
                    new FilterInfo(new ODataV3Attribute(), 0)
                };
        }

    }
}