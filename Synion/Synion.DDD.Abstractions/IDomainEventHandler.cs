using System.Threading;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions.Events;

namespace Synion.DDD.Abstractions
{
    public interface IDomainEventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : IDomainEvent
    {
        Task Handle(TEvent @event, CancellationToken cancellationToken);
    }
}