using System;
using System.Collections.Generic;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public interface IGetAggregateRootStatePort : IOutputPort<IGetAggregateRootStatePort.Query, IEnumerable<KeyValuePair<string, string>>>
    {
        public class Query : IQuery<IEnumerable<KeyValuePair<string, string>>>
        {
            public Query(Guid key)
            {
                Key = key;
            }

            public Guid Key { get; }
        }
    }
}