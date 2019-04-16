using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Stack.Core.Framework
{
    public class QueryMetadata<T> : IQueryMetadata, IEnumerable<T> where T : class
    {
        internal IEnumerable<T> Result;

        public QueryMetadata()
        {
            Result = new List<T>();
            StatusCode = HttpStatusCode.OK;
        }

        public QueryMetadata(IEnumerable<T> result, long? count)
        {
            Result = result ?? new List<T>();

            Count = count;
            Type = typeof(T);
            StatusCode = HttpStatusCode.OK;
        }

        public IEnumerable Results
        {
            get { return Result; }
        }

        public long? Count { get; set; }

        public Dictionary<string, string> ResultsMetaData { get; set; }

        public Type Type { get; set; }

	    public IEnumerator<T> GetEnumerator()
        {
            return Result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Result.GetEnumerator();
        }

        public void Add(T value)
        {
            var list = Result as List<T>;
            if(list==null) list = new List<T>();
            else list.AddRange(Result);

            list.Add(value);
            Result = list;
        }

        #region Error Results

        public HttpStatusCode StatusCode { get; internal set; }

        public string StatusMessage { get; internal set; }

        public static QueryMetadata<T> CreateResponseError(HttpStatusCode statusCode, string statusMessage)
        {
            var result = new QueryMetadata<T> {StatusCode = statusCode, StatusMessage = statusMessage, Count = 0};
            return result;
        }

        public static QueryMetadata<T> CreateResponseError(HttpStatusCode statusCode)
        {
            var result = new QueryMetadata<T> { StatusCode = statusCode, StatusMessage = ErrorMessages.GetMessages(statusCode), Count = 0 };
            return result;
        }

        #endregion Error Results

    }
}