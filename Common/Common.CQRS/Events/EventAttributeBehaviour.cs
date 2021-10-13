using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Events;
using Common.CQRS.Abstractions.Attributes;

namespace Common.CQRS.Events
{
    internal class EventAttributeBehaviour<TEvent> : AttributeBehaviour, IEventBehaviour<TEvent>
        where TEvent : IEvent
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Type @eventBehaviourType;

        public EventAttributeBehaviour(IServiceProvider serviceProvider) 
            : base()
        {   
            this.@eventBehaviourType = typeof(IEventBehaviour<>);
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task Handle(TEvent @event, IAttributeCollection attributeCollection, CancellationToken cancellationToken, EventBehaviourDelegate next)
        {
            Task nextDelegate() => next();

            var pipeline = this.GetBehaviourAttributes(attributeCollection)
                .Select(a => 
                    new { 
                        Attribute = a, 
                        Behaviour = GetEventAttributeBehaviour(a) 
                    }
                )
                .Aggregate(
                    seed: (EventBehaviourDelegate) nextDelegate,
                    func: (n, i) => () => i.Behaviour.Handle(@event, i.Attribute, cancellationToken, n)
                );

            return pipeline();
        }

        private IEventAttributeBehaviour<TEvent, BehaviourAttribute> GetEventAttributeBehaviour(BehaviourAttribute attribute)
        {
            return (IEventAttributeBehaviour<TEvent, BehaviourAttribute>)this.serviceProvider.GetRequiredService(
                serviceType: this.@eventBehaviourType.MakeGenericType(typeof(TEvent), attribute.GetType())
            );
        }
    }
}