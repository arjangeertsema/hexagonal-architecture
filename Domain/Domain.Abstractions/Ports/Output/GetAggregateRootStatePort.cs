using System;
using System.Collections.Generic;
using Synion.CQRS.Abstractions.Ports;

namespace Domain.Abstractions.Ports.Output
{
    public class GetAggregateRootStatePort : IOutputPort<IEnumerable<KeyValuePair<string, string>>>
    {
        public GetAggregateRootStatePort(Guid aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }

        public Guid AggregateRootId { get; }
    }
}