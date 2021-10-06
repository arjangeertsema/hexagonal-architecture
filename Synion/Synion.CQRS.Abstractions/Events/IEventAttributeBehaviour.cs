using System.Threading;
using System.Threading.Tasks;

namespace Synion.CQRS.Abstractions.Events
{
    public interface IEventAttributeBehaviour<TEvent, in TAttribute>
        where TEvent : IEvent
        where TAttribute : BehaviourAttribute
    {
        Task Handle(TEvent command, TAttribute attribute, CancellationToken cancellationToken, EventBehaviourDelegate next);
    }
}