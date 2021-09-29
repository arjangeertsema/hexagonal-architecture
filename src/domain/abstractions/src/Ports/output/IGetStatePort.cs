using System;
using System.Collections.Generic;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public interface IGetStatePort : IOutputPort<IGetStatePort.Query, IEnumerable<KeyValuePair<string, string>>>
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