using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Ports.Output;
using Synion.CQRS;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Ports;
using Synion.DDD.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Storage
{
    //Use transactional outbox pattern in production!
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class InMemoryDomainEventPublisher<TEvent> : IOutputPortHandler<PublishDomainEventPort<TEvent>>
        where TEvent : IDomainEvent
    {
        private readonly IMediator mediator;
        public InMemoryDomainEventPublisher(IMediator mediator)
        {
            this.mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));

        }
        public Task Handle(PublishDomainEventPort<TEvent> command, CancellationToken cancellationToken)
        {
            return mediator.Notify(command.Event, cancellationToken);
        }
    }
}