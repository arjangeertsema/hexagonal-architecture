using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.DDD;
using Reference.Domain.Abstractions.Ports.Output;

namespace Example.Adapters.DDD
{
    public class AggregateRootService : IOutputPort<SaveAggregateRootPort>
    {
        private readonly IMediator mediator;

        public AggregateRootService(IMediator mediator)
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.mediator = mediator;
        }

        public async Task Handle(SaveAggregateRootPort command)
        {
            var tasks = new List<Task>();
            
            foreach(var @event in command.AggregateRoot.GetChanges())
            {
                var handleDomainEvent = CreateHandleDomainEvent(command.CommandId, @event);
                tasks.Add(mediator.Send(handleDomainEvent));
            }

            await Task.WhenAll(tasks.ToArray());

            command.AggregateRoot.ClearChanges();
        }

        private ICommand CreateHandleDomainEvent(Guid commandId, IDomainEvent @event)
        {
            var type = typeof(HandleDomainEventPort<>);
            var genericType = type.MakeGenericType(@event.GetType());
            var command = Activator.CreateInstance(genericType, new object[] { commandId, @event });
            return command as ICommand;
        }
    }
}
