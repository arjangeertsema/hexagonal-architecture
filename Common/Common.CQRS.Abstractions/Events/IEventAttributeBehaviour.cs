using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions.Attributes;

namespace Common.CQRS.Abstractions.Events
{
    public interface IEventAttributeBehaviour<TEvent, in TAttribute>
        where TEvent : IEvent
        where TAttribute : BehaviourAttribute
    {
        Task Handle(TEvent command, TAttribute attribute, CancellationToken cancellationToken, EventBehaviourDelegate next);
    }
}