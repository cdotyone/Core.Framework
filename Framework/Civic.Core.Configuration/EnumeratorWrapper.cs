using System;
using System.Collections;
using System.Collections.Generic;

namespace Civic.Core.Configuration
{
    /// <devdoc>
    /// Represents a genereic enumerator
    /// </devdoc>	
    internal class EnumeratorWrapper<T> : IEnumerator<T>
    {
        private IEnumerator _wrappedEnumerator;

        internal EnumeratorWrapper(IEnumerator wrappedEnumerator)
        {
            _wrappedEnumerator = wrappedEnumerator;
        }

        T IEnumerator<T>.Current
        {
            get { return (T)_wrappedEnumerator.Current; }
        }

        void IDisposable.Dispose()
        {
            _wrappedEnumerator = null;
        }

        object IEnumerator.Current
        {
            get { return _wrappedEnumerator.Current; }
        }

        bool IEnumerator.MoveNext()
        {
            return _wrappedEnumerator.MoveNext();
        }

        void IEnumerator.Reset()
        {
            _wrappedEnumerator.Reset();
        }
    }
}
