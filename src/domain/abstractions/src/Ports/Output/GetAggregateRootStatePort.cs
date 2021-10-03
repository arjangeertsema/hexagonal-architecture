using System;
using System.Collections.Generic;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public class GetAggregateRootStatePort : IOutputPort<IEnumerable<KeyValuePair<string, string>>>
    {
        public GetAggregateRootStatePort(Guid key)
        {
            Key = key;
        }

        public Guid Key { get; }
    }
}