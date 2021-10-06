using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Events;

namespace Synion.CQRS.Events
{
    internal class BehaviourEventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : IEvent
    {
        private readonly IServiceProvider serviceProvider;

        public BehaviourEventHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task Handle(TEvent @event, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            var handlers = serviceProvider.GetRequiredService<IEnumerable<IEventHandler<TEvent>>>();
            foreach(var handler in handlers)
            {
                var attributes = GetHandlerAttributes(handler);
                Task handleDelegate() => handler.Handle(@event, cancellationToken);

                var pipeline = serviceProvider
                    .GetServices<IEventBehaviour<TEvent>>()
                    .Reverse()
                    .Aggregate(
                        seed: (EventBehaviourDelegate) handleDelegate, 
                        func: (next, behaviour) => () => behaviour.Handle(@event, attributes, cancellationToken, next)
                    );

                tasks.Add(pipeline());
            }

            return Task.WhenAll(tasks);
        }

        private static IAttributeCollection GetHandlerAttributes(IEventHandler<TEvent> handler) 
        {
            var name = nameof(IEventHandler<TEvent>.Handle);
            var reference = typeof(IEventHandler<TEvent>)
                .GetMethods()
                .Where(m => m.Name.Equals(name))
                .Single();

            var methodInfo = handler.GetType().GetMethod
            (
                name: reference.Name,
                types: reference.GetParameters()
                    .Select(p => p.ParameterType)
                    .ToArray()
            );

            if(methodInfo == null)
                throw new MissingMethodException(handler.GetType().FullName, reference.Name);

            var attributes = System.Attribute.GetCustomAttributes(methodInfo);
            return new AttributeCollection(attributes);
        }
    }
}