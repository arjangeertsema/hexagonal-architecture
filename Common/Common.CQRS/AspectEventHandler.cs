using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Events;
using Common.CQRS.Abstractions.Aspects;

namespace Common.CQRS
{
    internal class AspectEventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : IEvent
    {
        private readonly IServiceProvider serviceProvider;

        public AspectEventHandler(IServiceProvider serviceProvider)
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

                var aspects = serviceProvider
                    .GetServices<IEventAspect<TEvent>>()
                    .Reverse()
                    .Aggregate(
                        seed: (EventAspectDelegate) handleDelegate, 
                        func: (next, behaviour) => () => behaviour.Handle(@event, attributes, cancellationToken, next)
                    );

                tasks.Add(aspects());
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