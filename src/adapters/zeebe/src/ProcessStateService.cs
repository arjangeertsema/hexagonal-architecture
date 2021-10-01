using System.Collections.Generic;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Output;

namespace Reference.Adapters.Zeebe
{
    public class ProcessStateService : IOutputPortHandler<GetAggregateRootState, IEnumerable<KeyValuePair<string, string>>>
    {        public Task<IEnumerable<KeyValuePair<string, string>>> Handle(GetAggregateRootState query)
        {
            throw new System.NotImplementedException();
        }
    }
}