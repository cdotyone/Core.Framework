﻿using System;
using System.Security.Claims;
using System.Web;
using Civic.Framework.WebApi.Configuration;

namespace Civic.Framework.WebApi
{
    [Obsolete("Moved to Civic.Api.Security.AuthorizationHelper in Civic.Api")]
    public static class AuthorizationHelper
    {
        public static bool CanModify(string module, string entityname)
        {
            if (T4Config.GetCanModify(module, entityname)) return true;

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
            if (T4Config.GetCanAdd(module, entityname)) return true;

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

        [Obsolete("Please use CanRemove Instead")]
        public static bool CanDelete(string module, string entityname)
        {
            return CanRemove(module, entityname);
        }

        public static bool CanRemove(string module, string entityname)
        {
            if (T4Config.GetCanRemove(module, entityname)) return true;

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
            if (T4Config.GetCanView(module, entityname)) return true;

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

        public static bool HasPermission(string module, string entityname, string permission, bool exact = false)
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