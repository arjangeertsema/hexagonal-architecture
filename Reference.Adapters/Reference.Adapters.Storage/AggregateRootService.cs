using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Synion.DDD.Abstractions;
using Reference.Domain.Abstractions.Ports.Output;
using Synion.CQRS.Abstractions.Ports;
using Synion.CQRS.Abstractions.Commands;

namespace Example.Adapters.Storage
{
    public class AggregateRootService : IOutputPort<SaveAggregateRootPort>
    {
        private readonly IMediator mediator;

        public AggregateRootService(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public async Task Handle(SaveAggregateRootPort command)
        {
            var tasks = new List<Task>();
            var changes = command.AggregateRoot.Commit();
            
            foreach(var @event in changes)
            {
                var port = CreateHandleDomainEventPort(command.CommandId, @event);
                tasks.Add(mediator.Send(port));
            }

            await Task.WhenAll(tasks.ToArray());
        }

        private ICommand CreateHandleDomainEventPort(Guid commandId, IDomainEvent @event)
        {
            var type = typeof(HandleDomainEventPort<>);
            var genericType = type.MakeGenericType(@event.GetType());
            var command = Activator.CreateInstance(genericType, new object[] { commandId, @event });
            return command as ICommand;
        }
    }
}
