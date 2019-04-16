using System;
using System.Collections.Generic;
using Stack.Core.Audit;
using Stack.Core.Security;

namespace Core.Framework
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

        public bool OnAddBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            var info = EntityInfoManager.GetInfo(entity);
            if (!AuthorizationHelper.CanAdd(context.Who, info))
            {
                AuditManager.LogAccess(IdentityManager.GetUsername(context.Who), IdentityManager.GetClientMachine(context.Who), info.Module, info.Module, entity._key, null, null, entity, context.TransactionUID);
                throw new UnauthorizedAccessException();
            }
            return true;
        }

        public bool OnAddAfter<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnModifyBefore<T>(IEntityRequestContext context, T before, T after) where T : class, IEntityIdentity
        {
            var info = EntityInfoManager.GetInfo(before);
            if (!AuthorizationHelper.CanModify(context.Who, info))
            {
                AuditManager.LogAccess(IdentityManager.GetUsername(context.Who), IdentityManager.GetClientMachine(context.Who), info.Module, info.Module, before._key, null, null, before, context.TransactionUID);
                throw new UnauthorizedAccessException();
            }
            return true;
        }

        public bool OnModifyAfter<T>(IEntityRequestContext context, T before, T after) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnRemoveBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            var info = EntityInfoManager.GetInfo(entity);
            if (!AuthorizationHelper.CanRemove(context.Who, info))
            {
                AuditManager.LogAccess(IdentityManager.GetUsername(context.Who), IdentityManager.GetClientMachine(context.Who), info.Module, info.Module, entity._key, null, null, entity, context.TransactionUID);
                throw new UnauthorizedAccessException();
            }
            return true;
        }

        public bool OnRemoveAfter<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnGetBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            var info = EntityInfoManager.GetInfo(entity);
            if (!AuthorizationHelper.CanView(context.Who, info))
            {
                AuditManager.LogChange(IdentityManager.GetUsername(context.Who), IdentityManager.GetClientMachine(context.Who), info.Module, info.Module, info.Entity, null, null, null, "ACC", null, null, context.TransactionUID);
                throw new UnauthorizedAccessException();
            }
            return true;
        }

        public bool OnGetAfter<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnGetPagedBefore<T>(IEntityRequestContext context) where T : class, IEntityIdentity
        {
            var info = EntityInfoManager.GetInfo(typeof(T));
            if (!AuthorizationHelper.CanView(context.Who, info))
            {
                AuditManager.LogChange(IdentityManager.GetUsername(context.Who), IdentityManager.GetClientMachine(context.Who), info.Module, info.Module, info.Entity, null, null, null, "ACC", null, null, context.TransactionUID);
                throw new UnauthorizedAccessException();
            }
            return true;
        }

        public IEnumerable<T> OnGetPagedAfter<T>(IEntityRequestContext context, IEnumerable<T> list) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }
    }
}
