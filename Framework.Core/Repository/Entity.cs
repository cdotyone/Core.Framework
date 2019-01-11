using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Framework.Core
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
