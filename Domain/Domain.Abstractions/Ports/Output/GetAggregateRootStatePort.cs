using System;
using System.Collections.Generic;
using Common.CQRS.Abstractions.Queries;

namespace Domain.Abstractions.Ports.Output
{
    public class GetAggregateRootStatePort : IQuery<IEnumerable<KeyValuePair<string, string>>>
    {
        public GetAggregateRootStatePort(Guid aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }

        public Guid AggregateRootId { get; }
    }
}