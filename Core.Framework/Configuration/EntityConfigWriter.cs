using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stack.Core.Framework.Configuration
{
    public class EntityConfigWriter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value.GetType();

            if ((type != typeof(ConcurrentDictionary<string, ConcurrentDictionary<string, IEntityInfo>>)))
                serializer.Serialize(writer, value);

            var dict = value as ConcurrentDictionary<string, ConcurrentDictionary<string, IEntityInfo>>;
            var sorted = new SortedDictionary<string,SortedDictionary<string, IEntityInfo>>();

            foreach (var key in dict.Keys)
            {
                sorted[key] = new SortedDictionary<string, IEntityInfo>(dict[key]);
            }

            serializer.Serialize(writer, sorted);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(ConcurrentDictionary<string, ConcurrentDictionary<string, IEntityInfo>>)) ;
        }
    }
}
