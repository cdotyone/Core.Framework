using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civic.Core.Data;

namespace Civic.Framework.WebApi
{
    public interface IEntity2 : IEntity
    {
        void Add(IEntityService service);

        void Modify(IEntityService service);

        void Remove(IEntityService service);
    }
}
