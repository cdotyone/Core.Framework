using System.Security.Claims;
using System.Web;

namespace Civic.Framework.WebApi
{
    public static class AuthorizationHelper
    {
        public static bool CanModify(string module, string entityname)
        {
            var context = HttpContext.Current;
            var claimsPrincipal = context.User as ClaimsPrincipal;
            if (claimsPrincipal != null)
            {
                return
                    claimsPrincipal.IsInRole(module.ToUpperInvariant() + "." + entityname.ToUpperInvariant() + "_M") ||
                    claimsPrincipal.IsInRole(module.ToUpperInvariant() + "." + entityname.ToUpperInvariant() + "_F");
            }

            return false;
        }

        public static bool CanAdd(string module, string entityname)
        {
            var context = HttpContext.Current;
            var claimsPrincipal = context.User as ClaimsPrincipal;
            if (claimsPrincipal != null)
            {
                return
                    claimsPrincipal.IsInRole(module.ToUpperInvariant() + "." + entityname.ToUpperInvariant() + "_A") ||
                    claimsPrincipal.IsInRole(module.ToUpperInvariant() + "." + entityname.ToUpperInvariant() + "_F");
            }

            return false;
        }

        public static bool CanDelete(string module, string entityname)
        {
            var context = HttpContext.Current;
            var claimsPrincipal = context.User as ClaimsPrincipal;
            if (claimsPrincipal != null)
            {
                return
                    claimsPrincipal.IsInRole(module.ToUpperInvariant() + "." + entityname.ToUpperInvariant() + "_D") ||
                    claimsPrincipal.IsInRole(module.ToUpperInvariant() + "." + entityname.ToUpperInvariant() + "_F");
            }

            return false;
        }

        public static bool CanView(string module, string entityname)
        {
            var context = HttpContext.Current;
            var claimsPrincipal = context.User as ClaimsPrincipal;
            if (claimsPrincipal != null)
            {
                return
                    claimsPrincipal.IsInRole(module.ToUpperInvariant() + "." + entityname.ToUpperInvariant() + "_V") ||
                    claimsPrincipal.IsInRole(module.ToUpperInvariant() + "." + entityname.ToUpperInvariant() + "_F");
            }

            return false;
        }

        public static bool HasPermission(string module, string entityname, string permission, bool exact)
        {
            var context = HttpContext.Current;
            var claimsPrincipal = context.User as ClaimsPrincipal;
            if (claimsPrincipal != null)
            {
                return
                    claimsPrincipal.IsInRole(module.ToUpperInvariant() + "." + entityname.ToUpperInvariant() + "_"+ permission.ToUpperInvariant()) ||
                    (!exact && claimsPrincipal.IsInRole(module.ToUpperInvariant() + "." + entityname.ToUpperInvariant() + "_F"));
            }

            return false;
        }
    }
}
