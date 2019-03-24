using System.Security.Claims;

namespace Core.Framework
{
    public static class AuthorizationHelper
    {

        #region 2.0

        //public static bool CanModify(ClaimsPrincipal claimsPrincipal, string module, string entityname)
        //{
        //    if (EntityInfoManager.GetCanModify(module, entityname)) return true;
        //    if (claimsPrincipal != null)
        //    {
        //        var prefix = module.ToUpperInvariant() + "." + entityname.ToUpperInvariant();
        //        return
        //            claimsPrincipal.IsInRole(prefix + "_M") ||
        //            claimsPrincipal.IsInRole(prefix + "_F");
        //    }

        //    return false;
        //}

        //public static bool CanAdd(ClaimsPrincipal claimsPrincipal, string module, string entityname)
        //{
        //    if (EntityInfoManager.GetCanAdd(module, entityname)) return true;
        //    if (claimsPrincipal != null)
        //    {
        //        var prefix = module.ToUpperInvariant() + "." + entityname.ToUpperInvariant();
        //        return
        //            claimsPrincipal.IsInRole(prefix + "_A") ||
        //            claimsPrincipal.IsInRole(prefix + "_F");
        //    }

        //    return false;
        //}

        //public static bool CanRemove(ClaimsPrincipal claimsPrincipal, string module, string entityname)
        //{
        //    if (EntityInfoManager.GetCanRemove(module, entityname)) return true;
        //    if (claimsPrincipal != null)
        //    {
        //        var prefix = module.ToUpperInvariant() + "." + entityname.ToUpperInvariant();
        //        return
        //            claimsPrincipal.IsInRole(prefix + "_D") ||
        //            claimsPrincipal.IsInRole(prefix + "_F");
        //    }

        //    return false;
        //}

        //public static bool CanView(ClaimsPrincipal claimsPrincipal, string module, string entityname)
        //{
        //    if (EntityInfoManager.GetCanView(module, entityname)) return true;

        //    if (claimsPrincipal != null)
        //    {
        //        var prefix = module.ToUpperInvariant() + "." + entityname.ToUpperInvariant();
        //        return
        //            claimsPrincipal.IsInRole(prefix + "_V") ||
        //            claimsPrincipal.IsInRole(prefix + "_F");
        //    }

        //    return false;
        //}

        //public static bool HasPermission(ClaimsPrincipal claimsPrincipal, string module, string entityname, string permission, bool exact = false)
        //{
        //    if (claimsPrincipal != null)
        //    {
        //        var prefix = module.ToUpperInvariant() + "." + entityname.ToUpperInvariant();
        //        return
        //            claimsPrincipal.IsInRole(prefix + "_" + permission.ToUpperInvariant()) ||
        //            (!exact && claimsPrincipal.IsInRole(prefix + "_F"));
        //    }

        //    return false;
        //}

        #endregion 2.0

        #region 3.0

        public static bool CanModify(ClaimsPrincipal claimsPrincipal, IEntityInfo info)
        {
            if (EntityInfoManager.GetCanModify(info)) return true;
            if (claimsPrincipal != null)
            {
                var prefix = info.Name.ToUpperInvariant();
                return
                    claimsPrincipal.IsInRole(prefix + "_M") ||
                    claimsPrincipal.IsInRole(prefix + "_F");
            }
            return false;
        }

        public static bool CanAdd(ClaimsPrincipal claimsPrincipal, IEntityInfo info)
        {
            if (EntityInfoManager.GetCanAdd(info)) return true;
            if (claimsPrincipal != null)
            {
                var prefix = info.Name.ToUpperInvariant();
                return
                    claimsPrincipal.IsInRole(prefix + "_A") ||
                    claimsPrincipal.IsInRole(prefix + "_F");
            }

            return false;
        }

        public static bool CanRemove(ClaimsPrincipal claimsPrincipal, IEntityInfo info)
        {
            if (EntityInfoManager.GetCanRemove(info)) return true;
            if (claimsPrincipal != null)
            {
                var prefix = info.Name.ToUpperInvariant();
                return
                    claimsPrincipal.IsInRole(prefix + "_D") ||
                    claimsPrincipal.IsInRole(prefix + "_F");
            }

            return false;
        }

        public static bool CanView(ClaimsPrincipal claimsPrincipal, IEntityInfo info)
        {
            if (EntityInfoManager.GetCanView(info)) return true;

            if (claimsPrincipal != null)
            {
                var prefix = info.Name.ToUpperInvariant();
                return
                    claimsPrincipal.IsInRole(prefix + "_V") ||
                    claimsPrincipal.IsInRole(prefix + "_F");
            }

            return false;
        }

        public static bool HasPermission(ClaimsPrincipal claimsPrincipal, IEntityInfo info, string permission, bool exact = false)
        {
            if (claimsPrincipal != null)
            {
                var prefix = info.Name.ToUpperInvariant();
                return
                    claimsPrincipal.IsInRole(prefix + "_" + permission.ToUpperInvariant()) ||
                    (!exact && claimsPrincipal.IsInRole(prefix + "_F"));
            }

            return false;
        }

        #endregion 3.0

    }
}