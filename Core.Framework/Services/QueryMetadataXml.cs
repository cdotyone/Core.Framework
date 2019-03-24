using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SAAS.Core.Framework
{
    [Serializable]
    [DataContract(Name="data")]
    public class QueryMetadataXml<T> where T : class
    {
        public QueryMetadataXml()
        {
        }

        public QueryMetadataXml(IQueryMetadata data, string metadataUrl)
        {
            var list = data.Results as List<T>;
            if (list != null) Results = new List<T>(list);
            Count = data.Count;
            Metadata = metadataUrl;
        }

        [DataMember(Name = "value")]
        public List<T> Results { get; set; }

        [DataMember(Name = "count")]
        public long? Count { get; set; }

        [DataMember(Name = "metadata")]
        public string Metadata { get; set; }
    }
}