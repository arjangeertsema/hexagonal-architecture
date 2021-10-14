using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Aspects;
using Common.CQRS.Abstractions.Attributes;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Events;
using Common.CQRS.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Common.CQRS.Aspects
{
    internal abstract class AttributeAspect
    {
        private readonly Type behaviourAttributeType;

        public AttributeAspect()
        {
            this.behaviourAttributeType = typeof(AspectAttribute);            
        }

        protected IEnumerable<AspectAttribute> GetAspectAttributes(IEnumerable<Attribute> attributeCollection) 
        {
            return attributeCollection
                .Where(a => a.GetType().IsAssignableFrom(this.behaviourAttributeType))
                .Select(a => (AspectAttribute) a);
        }
    }

    internal class CommandAttributeAspect<TCommand> : AttributeAspect, ICommandAspect<TCommand>
        where TCommand : ICommand
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Type commandAspectType;

        public CommandAttributeAspect(IServiceProvider serviceProvider) 
            : base()
        {   
            this.commandAspectType = typeof(ICommandAspect<>);
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task Handle(TCommand command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, CommandAspectDelegate next)
        {
            Task nextDelegate() => next();

            var pipeline = this.GetAspectAttributes(attributeCollection)
                .Select(a => 
                    new { 
                        Attribute = a, 
                        Aspect = GetCommandAttributeAspect(a) 
                    }
                )
                .Aggregate(
                    seed: (CommandAspectDelegate) nextDelegate,
                    func: (n, i) => () => i.Aspect.Handle(command, i.Attribute, cancellationToken, n)
                );

            return pipeline();
        }

        private ICommandAttributeAspect<TCommand, AspectAttribute> GetCommandAttributeAspect(AspectAttribute attribute)
        {
            return (ICommandAttributeAspect<TCommand, AspectAttribute>)this.serviceProvider.GetRequiredService(
                serviceType: this.commandAspectType.MakeGenericType(typeof(TCommand), attribute.GetType())
            );
        }
    }

    internal class EventAttributeAspect<TEvent> : AttributeAspect, IEventAspect<TEvent>
        where TEvent : IEvent
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Type @eventAspectType;

        public EventAttributeAspect(IServiceProvider serviceProvider) 
            : base()
        {   
            this.@eventAspectType = typeof(IEventAspect<>);
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task Handle(TEvent @event, IAttributeCollection attributeCollection, CancellationToken cancellationToken, EventAspectDelegate next)
        {
            Task nextDelegate() => next();

            var pipeline = this.GetAspectAttributes(attributeCollection)
                .Select(a => 
                    new { 
                        Attribute = a, 
                        Aspect = GetEventAttributeAspect(a) 
                    }
                )
                .Aggregate(
                    seed: (EventAspectDelegate) nextDelegate,
                    func: (n, i) => () => i.Aspect.Handle(@event, i.Attribute, cancellationToken, n)
                );

            return pipeline();
        }

        private IEventAttributeAspect<TEvent, AspectAttribute> GetEventAttributeAspect(AspectAttribute attribute)
        {
            return (IEventAttributeAspect<TEvent, AspectAttribute>)this.serviceProvider.GetRequiredService(
                serviceType: this.@eventAspectType.MakeGenericType(typeof(TEvent), attribute.GetType())
            );
        }
    }
    internal class QueryAttributeAspect<TQuery, TResponse> : AttributeAspect, IQueryAspect<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Type queryAspectType;

        public QueryAttributeAspect(IServiceProvider serviceProvider) 
            : base()
        {   
            this.queryAspectType = typeof(IQueryAttributeAspect<,,>);
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task<TResponse> Handle(TQuery query, IAttributeCollection attributeCollection, CancellationToken cancellationToken, QueryAspectDelegate<TResponse> next)
        {
            Task<TResponse> nextDelegate() => next();

            var pipeline = this.GetAspectAttributes(attributeCollection)
                .Select(a => new { 
                    Attribute = a, 
                    Aspect = GetQueryAttributeAspect(a) 
                })
                .Aggregate(
                    seed: (QueryAspectDelegate<TResponse>) nextDelegate, 
                    func: (nxt, item) => ()  => item.Aspect.Handle(query, item.Attribute, cancellationToken, nxt)
                );

            return pipeline();
        }

        private IQueryAttributeAspect<TQuery, TResponse, AspectAttribute> GetQueryAttributeAspect(AspectAttribute attribute)
        {
            return (IQueryAttributeAspect<TQuery, TResponse, AspectAttribute>) this.serviceProvider.GetRequiredService
            (
                serviceType: this.queryAspectType.MakeGenericType(typeof(TQuery), typeof(TResponse), attribute.GetType())
            );
        }
    }
}