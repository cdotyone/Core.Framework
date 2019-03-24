using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Core.Framework
{
    public interface IEntityBusinessFacade<out T> where T : IEntityIdentity
    {
        IEnumerable<T> GetPaged(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy);

        T Get(IEntityRequestContext context, string key);

        T Get(IEntityRequestContext context, IEntityIdentity entity);

        void Save(IEntityRequestContext context, IEntityIdentity entity);

        void Remove(IEntityRequestContext context, IEntityIdentity entity);

        IEnumerable<T> GetPaged(ClaimsPrincipal who, int skip, ref int count, bool retCount, string filterBy, string orderBy);

        T Get(ClaimsPrincipal who, string key);

        T Get(ClaimsPrincipal who, IEntityIdentity entity);

        void Save(ClaimsPrincipal who, IEntityIdentity entity1);

        void Remove(ClaimsPrincipal who, string key);

        void Remove(ClaimsPrincipal who, IEntityIdentity entity);
    }

}
