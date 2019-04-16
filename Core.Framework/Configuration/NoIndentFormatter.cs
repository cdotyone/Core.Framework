using System;
using Newtonsoft.Json;

namespace Stack.Core.Framework.Configuration
{
    public class NoIndentFormatter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string[]) || objectType == typeof(T);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var settings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat, 
                NullValueHandling = NullValueHandling.Ignore
            };

            writer.WriteRawValue(JsonConvert.SerializeObject(value, Formatting.None, settings));
        }
    }
}
