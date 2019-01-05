using System;
using System.Collections.Generic;

namespace Civic.Framework.WebApi
{
    public class WriteOnlyHandler : IEntityEventHandler
    {
        public bool OnAddBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnAddAfter<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnModifyBefore<T>(IEntityRequestContext context, T before, T after) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnModifyAfter<T>(IEntityRequestContext context, T before, T after) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnRemoveBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            throw new UnauthorizedAccessException();
        }

        public bool OnRemoveAfter<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnGetBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            throw new UnauthorizedAccessException();
        }

        public bool OnGetAfter<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public bool OnGetPagedBefore<T>(IEntityRequestContext context) where T : class, IEntityIdentity
        {
            throw new UnauthorizedAccessException();
        }

        public IEnumerable<T> OnGetPagedAfter<T>(IEntityRequestContext context, IEnumerable<T> list) where T : class, IEntityIdentity
        {
            throw new NotImplementedException();
        }

        public EntityEventType Handlers
        {
            get { return EntityEventType.GetBefore | EntityEventType.GetPagedBefore; }
        }
    }
}
