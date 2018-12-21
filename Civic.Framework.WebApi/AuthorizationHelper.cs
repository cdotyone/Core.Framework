using System;
using System.Security.Claims;
using System.Web;
using Civic.Core.Security;
using Civic.Framework.WebApi.Configuration;

namespace Civic.Framework.WebApi
{
    public static class AuthorizationHelper
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


        public static bool CanModify(ClaimsPrincipal claimsPrincipal, IEntityIdentity entity)
        {
            return CanModify(claimsPrincipal, entity.schema,entity.entity);
        }

        public static bool CanAdd(ClaimsPrincipal claimsPrincipal, IEntityIdentity entity)
        {
            return CanModify(claimsPrincipal, entity.schema, entity.entity);
        }

        public static bool CanRemove(ClaimsPrincipal claimsPrincipal, IEntityIdentity entity)
        {
            return CanModify(claimsPrincipal, entity.schema, entity.entity);
        }

        public static bool CanView(ClaimsPrincipal claimsPrincipal, IEntityIdentity entity)
        {
            return CanModify(claimsPrincipal, entity.schema, entity.entity);
        }

        public static bool HasPermission(ClaimsPrincipal claimsPrincipal, IEntityIdentity entity, string permission, bool exact = false)
        {
            return HasPermission(claimsPrincipal, entity.schema, entity.entity, permission, exact);
        }


        public static bool CanModify(ClaimsPrincipal claimsPrincipal, string module, string entityname)
        {
            if (T4Config.GetCanModify(module, entityname)) return true;
            if (claimsPrincipal != null)
            {
                var prefix = module.ToUpperInvariant() + "." + entityname.ToUpperInvariant();
                return
                    claimsPrincipal.IsInRole(prefix + "_M") ||
                    claimsPrincipal.IsInRole(prefix + "_F");
            }

            return false;
        }

        public static bool CanAdd(ClaimsPrincipal claimsPrincipal, string module, string entityname)
        {
            if (T4Config.GetCanAdd(module, entityname)) return true;
            if (claimsPrincipal != null)
            {
                var prefix = module.ToUpperInvariant() + "." + entityname.ToUpperInvariant();
                return
                    claimsPrincipal.IsInRole(prefix + "_A") ||
                    claimsPrincipal.IsInRole(prefix + "_F");
            }

            return false;
        }

        public static bool CanRemove(ClaimsPrincipal claimsPrincipal, string module, string entityname)
        {
            if (T4Config.GetCanRemove(module, entityname)) return true;
            if (claimsPrincipal != null)
            {
                var prefix = module.ToUpperInvariant() + "." + entityname.ToUpperInvariant();
                return
                    claimsPrincipal.IsInRole(prefix + "_D") ||
                    claimsPrincipal.IsInRole(prefix + "_F");
            }

            return false;
        }

        public static bool CanView(ClaimsPrincipal claimsPrincipal, string module, string entityname)
        {
            if (T4Config.GetCanView(module, entityname)) return true;

            if (claimsPrincipal != null)
            {
                var prefix = module.ToUpperInvariant() + "." + entityname.ToUpperInvariant();
                return
                    claimsPrincipal.IsInRole(prefix + "_V") ||
                    claimsPrincipal.IsInRole(prefix + "_F");
            }

            return false;
        }

        public static bool HasPermission(ClaimsPrincipal claimsPrincipal, string module, string entityname, string permission, bool exact = false)
        {
            if (claimsPrincipal != null)
            {
                var prefix = module.ToUpperInvariant() + "." + entityname.ToUpperInvariant();
                return
                    claimsPrincipal.IsInRole(prefix + "_" + permission.ToUpperInvariant()) ||
                    (!exact && claimsPrincipal.IsInRole(prefix + "_F"));
            }

            return false;
        }
    }
}