using System.Collections.Generic;
using Newtonsoft.Json;

namespace SAAS.Core.Framework
{
    public interface IEntityIdentity
    {
        string _key { get; set; }

        #pragma warning disable IDE1006 // Naming Styles
        // ReSharper disable InconsistentNaming
        string _module { get; }

        string _entity { get; }

        Dictionary<string,object> _extra { get; set; }

        // ReSharper enable once InconsistentNaming
        #pragma warning restore IDE1006 // Naming Styles

        IEntityInfo GetInfo();

        IEntityIdentity LoadByKey(IEntityRequestContext context, string key);

        void RemoveByKey(IEntityRequestContext context, string key);

        IEnumerable<IEntityIdentity> GetPaged(IEntityRequestContext context, int skip, ref int count, bool retCount, string filterBy, string orderBy);

        IEntityIdentity Load(IEntityRequestContext context);

        void Save(IEntityRequestContext context);

        void Remove(IEntityRequestContext context);
    }
}
