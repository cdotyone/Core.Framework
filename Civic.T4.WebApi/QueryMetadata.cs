using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Civic.T4.WebApi
{
    public class QueryMetadata<T> : IQueryMetadata, IEnumerable<T> where T : class
    {
        internal IEnumerable<T> _result;

        public QueryMetadata()
        {
            _result = new List<T>();
        }

        public QueryMetadata(IEnumerable<T> result, long? count)
        {
            _result = result ?? new List<T>();

            Count = count;
            Type = typeof(T);
        }

        public IEnumerable Results
        {
            get { return _result; }
        }

        public long? Count { get; set; }

        public Type Type { get; set; }

	    public event QueryCallBack MetaDataAction;

	    public bool HasMetaDataAction {
			get
			{
				return MetaDataAction != null;
			}
	    }

		public void OnMetaRequest(ODataV3JsonFormatter formatter, IQueryMetadata data, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			if(MetaDataAction!=null)
				MetaDataAction(formatter, data, writeStream, content, transportContext);
		}

	    public IEnumerator<T> GetEnumerator()
        {
            return _result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _result.GetEnumerator();
        }

        public void Add(T value)
        {
            var list = _result as List<T>;
            if(list==null) list = new List<T>();
            else list.AddRange(_result);

            list.Add(value);
            _result = list;
        }
    }
}