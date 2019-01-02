using System;
using System.Collections.Generic;
using Civic.Core.Audit;
using Civic.Core.Security;

namespace Civic.Framework.WebApi
{
    public class BasicAuthorizationHandler : IEntityEventHandler
    {
        public EntityEventType Handlers
        {
            get
            {
                return EntityEventType.GetBefore | EntityEventType.GetPagedBefore | EntityEventType.AddBefore |
                       EntityEventType.ModifyBefore | EntityEventType.RemoveBefore;
            }
        }

        public bool OnAddBefore<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            if (!AuthorizationHelper.CanAdd(context.Who, info))
            {
                AuditManager.LogAccess<T>(IdentityManager.GetUsername(context.Who), IdentityManager.ClientMachine, info.Module, info.Module, entity._key, null, null, entity, context.TransactionUID);
                throw new UnauthorizedAccessException();
            }
            return true;
        }

        public bool OnAddAfter<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnModifyBefore<T>(IEntityRequestContext context, IEntityInfo info, T before, T after) where T : class, IEntityIdentity
        {
            if (!AuthorizationHelper.CanModify(context.Who, info))
            {
                AuditManager.LogAccess<T>(IdentityManager.GetUsername(context.Who), IdentityManager.ClientMachine, info.Module, info.Module, before._key, null, null, before, context.TransactionUID);
                throw new UnauthorizedAccessException();
            }
            return true;
        }

        public bool OnModifyAfter<T>(IEntityRequestContext context, IEntityInfo info, T before, T after) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnRemoveBefore<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            if (!AuthorizationHelper.CanRemove(context.Who, info))
            {
                AuditManager.LogAccess<T>(IdentityManager.GetUsername(context.Who), IdentityManager.ClientMachine, info.Module, info.Module, entity._key, null, null, entity, context.TransactionUID);
                throw new UnauthorizedAccessException();
            }
            return true;
        }

        public bool OnRemoveAfter<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnGetBefore(IEntityRequestContext context, IEntityInfo info)
        {
            if (!AuthorizationHelper.CanView(context.Who, info))
            {
                AuditManager.LogChange(IdentityManager.GetUsername(context.Who), IdentityManager.ClientMachine, info.Module, info.Module, info.Entity, null, null, null, "ACC", null, null, context.TransactionUID);
                throw new UnauthorizedAccessException();
            }
            return true;
        }

        public bool OnGetAfter<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnGetPagedBefore(IEntityRequestContext context, IEntityInfo info)
        {
            if (!AuthorizationHelper.CanView(context.Who, info))
            {
                AuditManager.LogChange(IdentityManager.GetUsername(context.Who), IdentityManager.ClientMachine, info.Module, info.Module, info.Entity, null, null, null, "ACC", null, null, context.TransactionUID);
                throw new UnauthorizedAccessException();
            }
            return true;
        }

        public List<T> OnGetPagedAfter<T>(IEntityRequestContext context, IEntityInfo info, List<T> list) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }
    }
}
