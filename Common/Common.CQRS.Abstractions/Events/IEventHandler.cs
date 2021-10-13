using System.Threading;
using System.Threading.Tasks;

namespace Common.CQRS.Abstractions.Events
{
    public interface IEventHandler<TEvent>
        where TEvent : IEvent
    {
        Task Handle(TEvent @event, CancellationToken cancellationToken);
    }
}