using System;
using System.Collections;
using System.Collections.Generic;

namespace Civic.Core.WebApi
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