using System.Collections.Generic;
using System.Threading.Tasks;
using Reference.Domain.Abstractions.Ports.Output;

namespace Reference.Adapters.Zeebe
{
    public class ProcessStateService : IGetAggregateRootStatePort
    {
        public Task<IEnumerable<KeyValuePair<string, string>>> Execute(IGetAggregateRootStatePort.Query query)
        {
            //Get process variables from elastic search and convert to IEnumerable<KeyValuePair<string, string>>
            
            throw new System.NotImplementedException();
        }
    }
}