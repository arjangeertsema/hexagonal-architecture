using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions.Events;

namespace Common.DDD.Abstractions
{
    public interface IDomainEventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : IDomainEvent
    {
        Task Handle(TEvent @event, CancellationToken cancellationToken);
    }
}