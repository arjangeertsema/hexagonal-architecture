using System;
using System.Collections.Generic;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public class GetAggregateRootState : IOutputPort<IEnumerable<KeyValuePair<string, string>>>
    {
        public GetAggregateRootState(Guid key)
        {
            Key = key;
        }

        public Guid Key { get; }
    }
}