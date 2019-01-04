using System.Collections.Generic;

namespace Civic.Framework.WebApi
{
    public interface IEntityEventHandler
    {
        EntityEventType Handlers { get; }

        bool OnAddBefore<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity;
        bool OnAddAfter<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity;
        bool OnModifyBefore<T>(IEntityRequestContext context, IEntityInfo info, T before, T after) where T : class, IEntityIdentity;
        bool OnModifyAfter<T>(IEntityRequestContext context, IEntityInfo info, T before, T after) where T : class, IEntityIdentity;
        bool OnRemoveBefore<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity;
        bool OnRemoveAfter<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity;

        bool OnGetBefore<T>(IEntityRequestContext context, IEntityInfo info, T entity);
        bool OnGetAfter<T>(IEntityRequestContext context, IEntityInfo info, T entity) where T : class, IEntityIdentity;
        bool OnGetPagedBefore(IEntityRequestContext context, IEntityInfo info);
        IEnumerable<T> OnGetPagedAfter<T>(IEntityRequestContext context, IEntityInfo info, IEnumerable<T> list) where T : class, IEntityIdentity;
    }
}
