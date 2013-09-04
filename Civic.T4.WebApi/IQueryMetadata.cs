using System;
using System.Collections;

namespace Civic.T4.WebApi
{
    public interface IQueryMetadata
    {
        IEnumerable Results { get; }

        long? Count { get; }

        Type Type { get; }
    }
}