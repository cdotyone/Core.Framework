using System;
using Civic.Core.Data;

namespace Civic.Framework.WebApi
{
    [Obsolete("Generation 3 now available")]
    public interface IEntity
    {
        string IdentityID { get; }

        void Add(IDBConnection connection);

        void Modify(IDBConnection connection);

        void Remove(IDBConnection connection);
    }
}
