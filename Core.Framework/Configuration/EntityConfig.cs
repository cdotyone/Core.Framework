using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Core.Logging;
using Newtonsoft.Json;

namespace Core.Framework.Configuration
{
    [DataContract(Name = "entityConfig")]
    public class EntityConfig
    {
        [DataMember(Name = "defaults")]
        public EntityDefaults Defaults { get; set; } = new EntityDefaults();

        [DataMember(Name = "hash")]
        public string Hash { get; set; }

        [DataMember(Name = "entities")]
        public ConcurrentDictionary<string, ConcurrentDictionary<string, IEntityInfo>> Entities { get; set; } = new ConcurrentDictionary<string, ConcurrentDictionary<string,IEntityInfo>>();

        [JsonIgnore]
        public bool Changed { get; private set; }

        private String GetHash<T>(Stream stream) where T : HashAlgorithm, new()
        {
            StringBuilder sb = new StringBuilder();
            using (T crypt = new T())
            {
                byte[] hashBytes = crypt.ComputeHash(stream); foreach (byte bt in hashBytes) { sb.Append(bt.ToString("x2")); }
            } return sb.ToString();
        }


        public string ComputeHash()
        {
            var settings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat
                ,NullValueHandling = NullValueHandling.Ignore
                ,Formatting = Formatting.Indented
                ,Converters = new List<JsonConverter>
                {
                    new NoIndentFormatter<EntityPropertyInfo>(),
                    new EntityConfigWriter()
                }
            };

            var oldHash = Hash;
            Hash = null;
            var json = JsonConvert.SerializeObject(this, settings);
            Hash = oldHash;

            string newHash;
            using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                newHash = GetHash<MD5Cng>(stream);
            }

            return newHash;
        }

        public void Load(string filename)
        {
            var settings = new JsonSerializerSettings
            {
                 DateFormatHandling = DateFormatHandling.IsoDateFormat
                ,NullValueHandling = NullValueHandling.Ignore
            };

            settings.Converters.Add(new EntityConfigReader());

            JsonConvert.PopulateObject(File.ReadAllText(filename), this, settings);

            var hash = ComputeHash();

            Changed = hash != Hash;

            if (Changed)
            {
                Logger.LogInformation(LoggingBoundaries.Host, $"Configuration file {filename} has changed");
            }
        }

        public void Save(string filename)
        {
            var newHash = ComputeHash();
            if (Hash == newHash)
            {
                Logger.LogInformation(LoggingBoundaries.Host, $"No Detectable Change In {filename}");
                return;
            }
            Hash = newHash;

            var settings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat
                ,NullValueHandling = NullValueHandling.Ignore
                ,Formatting = Formatting.Indented
                ,Converters = new List<JsonConverter>
                {
                    new NoIndentFormatter<EntityPropertyInfo>(),
                    new EntityConfigWriter()
                }
            };

            File.WriteAllText(filename,JsonConvert.SerializeObject(this, settings));
        }
    }
}
