using System.Security.Claims;
using Civic.Framework.WebApi.Configuration;

namespace Civic.Framework.WebApi
{
    public static partial class AuthorizationHelper
    {

        #region 2.0

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

        #endregion 2.0

        #region 3.0

        public static bool CanModify(ClaimsPrincipal claimsPrincipal, IEntityInfo entity)
        {
            return CanModify(claimsPrincipal, entity.Module, entity.Entity);
        }

        public static bool CanAdd(ClaimsPrincipal claimsPrincipal, IEntityInfo entity)
        {
            return CanAdd(claimsPrincipal, entity.Module, entity.Entity);
        }

        public static bool CanRemove(ClaimsPrincipal claimsPrincipal, IEntityInfo entity)
        {
            return CanRemove(claimsPrincipal, entity.Module, entity.Entity);
        }

        public static bool CanView(ClaimsPrincipal claimsPrincipal, IEntityInfo entity)
        {
            return CanView(claimsPrincipal, entity.Module, entity.Entity);
        }

        public static bool HasPermission(ClaimsPrincipal claimsPrincipal, IEntityInfo entity, string permission, bool exact = false)
        {
            return HasPermission(claimsPrincipal, entity.Module, entity.Entity, permission, exact);
        }

        #endregion 3.0

    }
}