using System;
using System.Security.Claims;
using System.Web;
using Civic.Core.Security;
using Civic.Framework.WebApi.Configuration;

namespace Civic.Framework.WebApi
{
    public static partial class AuthorizationHelper
    {
        [Obsolete("use claims principle versions")]
        public static bool CanModify(string module, string entityname)
        {
            if (T4Config.GetCanModify(module, entityname)) return true;

            var context = HttpContext.Current;
            if (context == null) return true; // NOT a web users, security to be handled by application

            var claimsPrincipal = context.User as ClaimsPrincipal;

            return CanModify(claimsPrincipal, module, entityname);
        }

        [Obsolete("use claims principle versions")]
        public static bool CanAdd(string module, string entityname)
        {
            if (T4Config.GetCanAdd(module, entityname)) return true;

            var context = HttpContext.Current;
            if (context == null) return true; // NOT a web users, security to be handled by application

            var claimsPrincipal = context.User as ClaimsPrincipal;

            return CanAdd(claimsPrincipal, module, entityname);
        }

        [Obsolete("Please use CanRemove Instead")]
        public static bool CanDelete(string module, string entityname)
        {
            return CanRemove(module, entityname);
        }

        [Obsolete("use claims principle versions")]
        public static bool CanRemove(string module, string entityname)
        {
            if (T4Config.GetCanRemove(module, entityname)) return true;

            var context = HttpContext.Current;
            if (context == null) return true; // NOT a web users, security to be handled by application

            var claimsPrincipal = context.User as ClaimsPrincipal;

            return CanRemove(claimsPrincipal, module, entityname);
        }

        [Obsolete("use claims principle versions")]
        public static bool CanView(string module, string entityname)
        {
            if (T4Config.GetCanView(module, entityname)) return true;

            var context = HttpContext.Current;
            if (context == null) return true; // NOT a web users, security to be handled by application

            var claimsPrincipal = context.User as ClaimsPrincipal;

            return CanView(claimsPrincipal, module, entityname);
        }

        [Obsolete("use claims principle versions")]
        public static bool HasPermission(string module, string entityname, string permission, bool exact = false)
        {
            var context = HttpContext.Current;
            if (context == null) return true; // NOT a web users, security to be handled by application

            var claimsPrincipal = context.User as ClaimsPrincipal;

            return HasPermission(claimsPrincipal, module, entityname, permission, exact);
        }
    }
}