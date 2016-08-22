using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civic.Core.Data;

namespace Civic.Framework.WebApi
{
    public interface IEntity
    {
        string IdentityID { get; }

        void Add(IDBConnection connection);

        void Modify(IDBConnection connection);

        void Remove(IDBConnection connection);
    }
}
