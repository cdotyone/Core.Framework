using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Core.Framework
{
    public interface IQueryMetadata
    {
		IEnumerable Results { get; }

        long? Count { get; }

        Dictionary<string, string> ResultsMetaData { get; set; }

        Type Type { get; }

        HttpStatusCode StatusCode { get; }

        string StatusMessage { get; }
    }
}