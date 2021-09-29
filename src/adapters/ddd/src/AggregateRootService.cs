using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Reference.Domain.Abstractions.DDD;
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
            var tasks = new List<Task>();
            
            foreach(var @event in command.AggregateRoot.GetChanges())
            {
                var type = GetDomainEventHandlerType(@event);
                var handlers = GetDomainEventHandlers(@event, type);

                foreach (var handler in handlers)
                {
                    var task = type.GetMethod("").Invoke(handler, new object[] { @event }) as Task;
                    tasks.Add(task);
                }
            }

            await Task.WhenAll(tasks.ToArray());

            command.AggregateRoot.ClearChanges();
        }

        private object[] GetDomainEventHandlers(IDomainEvent @event, Type type)
        {
            var handlers = serviceProvider.GetServices(type)
                                .Where(h => h != null)
                                .ToArray();

            if (handlers == null || handlers.Length == 0)
            {
                throw new DomainEventHandlerNotFoundException(@event);
            }

            return handlers;
        }

        private Type GetDomainEventHandlerType(IDomainEvent @event) 
        {
            var type = typeof(IHandleDomainEventPort<>);
            var genericType = type.MakeGenericType(@event.GetType());
            return genericType;
        }
    }
}
