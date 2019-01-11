using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Framework.Core.OData;

namespace Framework.Core
{
	public delegate void QueryCallBack(ODataV3JsonFormatter formatter, IQueryMetadata data, Stream writeStream, HttpContent content, TransportContext transportContext);

    public interface IQueryMetadata
    {
        
		IEnumerable Results { get; }

        long? Count { get; }

        Dictionary<string, string> ResultsMetaData { get; set; }

        Type Type { get; }

	    event QueryCallBack MetaDataAction;

		bool HasMetaDataAction { get; }

		void OnMetaRequest(ODataV3JsonFormatter formatter, IQueryMetadata data, Stream writeStream, HttpContent content, TransportContext transportContext);

        HttpStatusCode StatusCode { get; }

        string StatusMessage { get; }
    }
}