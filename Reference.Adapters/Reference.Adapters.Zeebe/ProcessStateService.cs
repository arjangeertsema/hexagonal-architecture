using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Reference.Domain.Abstractions.Ports.Output;
using Synion.CQRS.Abstractions.Ports;

namespace Reference.Adapters.Zeebe
{
    public class ProcessStateService : IOutputPortHandler<GetAggregateRootStatePort, IEnumerable<KeyValuePair<string, string>>>
    {        
        public Task<IEnumerable<KeyValuePair<string, string>>> Handle(GetAggregateRootStatePort query, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}