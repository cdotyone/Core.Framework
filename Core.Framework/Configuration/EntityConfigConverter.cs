using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Framework.Configuration
{
    public class EntityConfigConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value.GetType();

            if ((type != typeof(ConcurrentDictionary<string, IEntityInfo>)))
                serializer.Serialize(writer, value);

            var dict = value as ConcurrentDictionary<string, IEntityInfo>;
            var sorted = new SortedDictionary<string, IEntityInfo>(dict);

            serializer.Serialize(writer, sorted);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            if((objectType == typeof(IEntityInfo))) 
                return serializer.Deserialize(reader, typeof(EntityInfo));

            if((objectType == typeof(IEntityPropertyInfo))) 
                return serializer.Deserialize(reader, typeof(EntityPropertyInfo));

            if ((objectType == typeof(ConcurrentDictionary<string, IEntityInfo>)))
                return serializer.Deserialize(reader, typeof(ConcurrentDictionary<string, IEntityInfo>));

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IEntityInfo)) || (objectType == typeof(IEntityPropertyInfo)  || (objectType == typeof(ConcurrentDictionary<string, IEntityInfo>))) ;
        }
    }
}
