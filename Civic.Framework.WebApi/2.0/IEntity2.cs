using System;

namespace Civic.Framework.WebApi
{
    [Obsolete("Generation 3 now available")]
    public interface IEntity2 : IEntity
    {
        void Add(IEntityService service);

        void Modify(IEntityService service);

        void Remove(IEntityService service);
    }
}
