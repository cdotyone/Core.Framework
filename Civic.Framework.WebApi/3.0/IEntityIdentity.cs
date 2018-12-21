using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Civic.Framework.WebApi
{
    public interface IEntityIdentity
    {
        [JsonIgnore]
        string IdentityID { get; }

        #pragma warning disable IDE1006 // Naming Styles
        // ReSharper disable InconsistentNaming
        string schema { get; }

        string entity { get; }
        // ReSharper enable once InconsistentNaming
        #pragma warning restore IDE1006 // Naming Styles
    }
}
