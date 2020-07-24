using System;
using Newtonsoft.Json;

namespace Core.Framework.Configuration
{
    public class EntityConfigReader : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            if((objectType == typeof(IEntityInfo))) 
                return serializer.Deserialize(reader, typeof(EntityInfo));

            if((objectType == typeof(IEntityPropertyInfo))) 
                return serializer.Deserialize(reader, typeof(EntityPropertyInfo));

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IEntityInfo)) || (objectType == typeof(IEntityPropertyInfo)) ;
        }
    }
}
