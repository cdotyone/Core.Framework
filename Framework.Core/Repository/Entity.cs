using System.Collections.Generic;
using System.Runtime.Serialization;
using Civic.Framework.DynamicEntity.Api.Entities;
using Newtonsoft.Json;

namespace Civic.Framework.WebApi
{
	[JsonConverter(typeof (EntityConverter))]
	public class Entity
	{
		[DataMember(Name = "Properties")]
		public Dictionary<string, object> Properties { get; set; }

		[DataMember(Name = "Names")]
		public Dictionary<string, IEntityPropertyInfo> PropertyNames { get; set; }
	}
}
