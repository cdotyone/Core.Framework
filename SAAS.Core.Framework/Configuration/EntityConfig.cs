using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SAAS.Core.Framework.Configuration
{
    [DataContract(Name = "entityConfig")]
    public class EntityConfig
    {
        [DataMember(Name = "defaults")]
        public EntityDefaults Defaults { get; set; } = new EntityDefaults();

        [DataMember(Name = "entities")]
        public ConcurrentDictionary<string, ConcurrentDictionary<string, IEntityInfo>> Entities { get; set; } = new ConcurrentDictionary<string, ConcurrentDictionary<string,IEntityInfo>>();

        public void Load(string filename)
        {
            var settings = new JsonSerializerSettings
            {
                 DateFormatHandling = DateFormatHandling.IsoDateFormat
                ,NullValueHandling = NullValueHandling.Ignore
            };

            JsonConvert.PopulateObject(File.ReadAllText(filename), this, settings);
        }

        public void Save(string filename)
        {

            var settings = new JsonSerializerSettings
            {
                 DateFormatHandling = DateFormatHandling.IsoDateFormat
                ,NullValueHandling = NullValueHandling.Ignore
                ,Formatting = Formatting.Indented
                ,Converters = new List<JsonConverter>
                {
                    new NoIndentFormatter<EntityPropertyInfo>()
                }
            };

            File.WriteAllText(filename,JsonConvert.SerializeObject(this, settings));
        }
    }
}
