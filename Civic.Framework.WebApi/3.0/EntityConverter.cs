using System;
using Civic.Framework.WebApi;
using Newtonsoft.Json;

namespace Civic.Framework.DynamicEntity.Api.Entities
{
	public class EntityConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var entity = value as Entity;
			if (entity != null)
			{
				if (entity.Properties != null)
				{
					writer.WriteStartObject();
					foreach (var pair in entity.Properties)
					{
						var name = pair.Key;

						if (name.StartsWith("_")) { name = name.TrimStart(new[] {'_'}).ToLower();} 
						else name = name.Substring(0, 1).ToLower() + name.Substring(1);

						writer.WritePropertyName(name);

						var type = pair.Value.GetType().Name.ToLower();
						switch (type)
						{
							case "decimal":
								writer.WriteValue((decimal)pair.Value);
								break;
							case "int16":
								writer.WriteValue((Int16)pair.Value);
								break;
							case "int32":
								writer.WriteValue((Int32)pair.Value);
								break;
							case "int64":
								writer.WriteValue((Int64)pair.Value);
								break;
							case "double":
								writer.WriteValue((double)pair.Value);
								break;
							case "float":
								writer.WriteValue((float)pair.Value);
								break;
							case "datetime":
								writer.WriteValue((DateTime)pair.Value);
								break;
							case "boolean":
								writer.WriteValue((Boolean)pair.Value);
								break;
							default:
								writer.WriteValue(pair.Value.ToString());
								break;
						}
					}
					writer.WriteEndObject();
				}
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			/*			User user = new User();
						user.UserName = (string)reader.Value;

						return user*/
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof (Entity);
		}
	}
}