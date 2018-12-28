using System;
using System.Security.Claims;
using System.Web;
using Civic.Core.Security;
using Civic.Framework.WebApi.Configuration;

namespace Civic.Framework.WebApi
{
    public static partial class AuthorizationHelper
    {
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
    }
}