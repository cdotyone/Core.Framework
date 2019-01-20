using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace SAAS.Core.Framework
{
    public interface IEntityBusinessFacade<T> where T : IEntityIdentity
    {
        IEnumerable<T> GetPaged(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy);

        T Get(IEntityRequestContext context, string key);

        T Get(IEntityRequestContext context, T entity);

        void Save(IEntityRequestContext context, T entity);

        void Remove(IEntityRequestContext context, T entity);

        IEnumerable<T> GetPaged(ClaimsPrincipal who, int skip, ref int count, bool retCount, string filterBy, string orderBy);

        T Get(ClaimsPrincipal who, string key);

        T Get(ClaimsPrincipal who, T entity);

        void Save(ClaimsPrincipal who, T entity1);

        void Remove(ClaimsPrincipal who, string key);

        void Remove(ClaimsPrincipal who, T entity);
    }

}
