using System;
using System.Collections;

namespace Civic.Core.WebApi
{
    public interface IQueryMetadata
    {
        IEnumerable Results { get; }

        long? Count { get; }

        Type Type { get; }
    }
}