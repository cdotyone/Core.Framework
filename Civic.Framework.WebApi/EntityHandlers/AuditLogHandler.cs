using System;
using System.Collections.Generic;
using System.Security.Claims;
using Civic.Core.Audit;
using Civic.Core.Security;

namespace Civic.Framework.WebApi
{
    public class AuditLogHandler : IEntityEventHandler
    {
        public EntityEventType Handlers
        {
            get
            {
                return EntityEventType.AddAfter | 
                       EntityEventType.ModifyAfter | 
                       EntityEventType.RemoveAfter;
            }
        }

        public bool OnAddBefore<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnAddAfter<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            AuditManager.LogAdd<T>(IdentityManager.GetUsername(context.Who), IdentityManager.ClientMachine, info.Module, info.Module, entity._key, null, null, entity, context.TransactionUID);
            return true;
        }

        public bool OnModifyBefore<T>(IEntityRequestContext context, IEntityInfo info, T before, T after) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnModifyAfter<T>(IEntityRequestContext context, IEntityInfo info, T before, T after) where T : class, IEntityIdentity
        {
            AuditManager.LogModify<T>(IdentityManager.GetUsername(context.Who), IdentityManager.ClientMachine, info.Module, info.Module, before._key, null, null, before, after, context.TransactionUID);
            return true;
        }

        public bool OnRemoveBefore<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnRemoveAfter<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            AuditManager.LogRemove<T>(IdentityManager.GetUsername(context.Who), IdentityManager.ClientMachine, info.Module, info.Module, entity._key, null, null, entity, context.TransactionUID);
            return true;
        }

        public bool OnGetBefore<T>(IEntityRequestContext context, IEntityInfo info, T entity)
        {
            throw new NotImplementedException();
        }

        public bool OnGetAfter<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnGetPagedBefore(IEntityRequestContext context, IEntityInfo info)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> OnGetPagedAfter<T>(IEntityRequestContext context, IEntityInfo info, IEnumerable<T> list) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

    }
}
