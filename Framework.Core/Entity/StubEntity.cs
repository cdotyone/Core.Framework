using System.Collections.Generic;
using Newtonsoft.Json;

namespace Civic.Framework.WebApi
{
    public class StubEntity<T> : IEntityIdentity
    {
        [JsonExtensionData]
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        public string _key { get; set; }
        public string _module { get; }
        public string _entity { get; }
        public IEntityInfo GetInfo()
        {
            throw new System.NotImplementedException();
        }

        public IEntityIdentity LoadByKey(IEntityRequestContext context, string key)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveByKey(IEntityRequestContext context, string key)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IEntityIdentity> GetPaged(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy,
            string orderBy)
        {
            throw new System.NotImplementedException();
        }

        public IEntityIdentity Load(IEntityRequestContext context)
        {
            throw new System.NotImplementedException();
        }

        public void Save(IEntityRequestContext context)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(IEntityRequestContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
