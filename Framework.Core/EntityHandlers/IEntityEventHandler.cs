using System.Collections.Generic;

namespace Civic.Framework.WebApi
{
    public interface IEntityEventHandler
    {
        EntityEventType Handlers { get; }

        bool OnAddBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity;
        bool OnAddAfter<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity;
        bool OnModifyBefore<T>(IEntityRequestContext context, T before, T after) where T : class, IEntityIdentity;
        bool OnModifyAfter<T>(IEntityRequestContext context, T before, T after) where T : class, IEntityIdentity;
        bool OnRemoveBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity;
        bool OnRemoveAfter<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity;

        bool OnGetBefore<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity;
        bool OnGetAfter<T>(IEntityRequestContext context, T entity) where T : class, IEntityIdentity;
        bool OnGetPagedBefore<T>(IEntityRequestContext context) where T : class, IEntityIdentity;

        IEnumerable<T> OnGetPagedAfter<T>(IEntityRequestContext context, IEnumerable<T> list) where T : class, IEntityIdentity;
    }
}
