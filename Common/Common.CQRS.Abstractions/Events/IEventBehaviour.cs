using System.Threading;
using System.Threading.Tasks;

namespace Common.CQRS.Abstractions.Events
{
    public delegate Task EventBehaviourDelegate();
    
    public interface IEventBehaviour<in TEvent>
        where TEvent : notnull, IEvent
    {
        Task Handle(TEvent command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, EventBehaviourDelegate next);
    }
}