using Newtonsoft.Json;

namespace Civic.Framework.WebApi
{
    public interface IEntityIdentity
    {
        [JsonIgnore]
        string IdentityID { get; }

        #pragma warning disable IDE1006 // Naming Styles
        // ReSharper disable InconsistentNaming
        string _module { get; }

        string _entity { get;  }
        // ReSharper enable once InconsistentNaming
        #pragma warning restore IDE1006 // Naming Styles

        IEntityInfo GetInfo();

        void Load(IEntityRequestContext context);

        void Save(IEntityRequestContext context);

        void Remove(IEntityRequestContext context);
    }
}
