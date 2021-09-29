using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions.Ports.Output;

namespace Example.Adapters.DDD
{
    public class AggregateRootService : ISaveAggregateRootPort
    {
        private readonly IServiceProvider serviceProvider;

        public AggregateRootService(IServiceProvider serviceProvider)
        {
            if (serviceProvider is null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            this.serviceProvider = serviceProvider;
        }
        public async Task Execute(ISaveAggregateRootPort.Command command)
        {
            foreach(var @event in command.AggregateRoot.GetChanges())
            {
                //TODO: reflect on event and handler.
            }
            command.AggregateRoot.GetChanges();
        }
    }
}
