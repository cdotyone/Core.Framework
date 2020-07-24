using System.Collections.Generic;

namespace Core.Framework
{
    
    public interface IEntityRepository<T> where T : IEntityIdentity
    {
        IEnumerable<T> GetPaged(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy );
    
    	T Get(IEntityRequestContext context, T entity);
    
    	void Add(IEntityRequestContext context, T entity );
    
    	void Modify(IEntityRequestContext context, T before, T after );
    
    	void Remove(IEntityRequestContext context, T entity );
    }
}

