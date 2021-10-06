using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Events;
using Synion.CQRS.Abstractions.Queries;
using Synion.CQRS.Commands;
using Synion.CQRS.Events;
using Synion.CQRS.Queries;

namespace Synion.CQRS
{
    public class Mediator : IMediator
    {
        private readonly MethodInfo sendCommandMethod;
        private readonly MethodInfo sendQueryMethod;
        private readonly MethodInfo notifyEventMethod;
        private readonly IServiceProvider serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            var type = this.GetType();            
            this.sendCommandMethod = type.GetMethod(nameof(SendCommand));
            this.sendQueryMethod = type.GetMethod(nameof(SendQuery));
            this.notifyEventMethod = type.GetMethod(nameof(NotifyEvent));

            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task Send(ICommand command, CancellationToken cancellationToken = default)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return (Task) this.sendCommandMethod
                .MakeGenericMethod(command.GetType())
                .Invoke(this, new object[] { command, cancellationToken });
        }

        public Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            
            return (Task<TResponse>) this.sendQueryMethod
                .MakeGenericMethod(query.GetType(), typeof(TResponse))
                .Invoke(this, new object[] { query, cancellationToken });
        }

        public Task Notify(IEvent @event, CancellationToken cancellationToken = default)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            return (Task) this.notifyEventMethod
                .MakeGenericMethod(@event.GetType())
                .Invoke(this, new object[] { @event, cancellationToken });
        }

        private Task SendCommand<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand
        {
            var handler = serviceProvider.GetRequiredService<BehaviourCommandHandler<TCommand>>();
            return handler.Handle(command, cancellationToken);
        }

        public Task<TResponse> SendQuery<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResponse>
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var handler = serviceProvider.GetRequiredService<BehaviourQueryHandler<TQuery, TResponse>>();
            return handler.Handle(query, cancellationToken);
        }

        private Task NotifyEvent<TEvent>(TEvent @event, CancellationToken cancellationToken)
            where TEvent : IEvent
        {
            var handler = serviceProvider.GetRequiredService<BehaviourEventHandler<TEvent>>();
            return handler.Handle(@event, cancellationToken);
        }
    }
}