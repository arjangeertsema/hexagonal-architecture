using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Queries;

namespace Synion.CQRS
{
    public class Mediator : IMediator
    {
        private readonly Type commandHandlerType;
        private readonly Type queryHandlerType;
        private readonly MethodInfo sendCommandMethod;
        private readonly MethodInfo sendQueryMethod;
        private readonly IServiceProvider serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            var type = this.GetType();            
            this.sendCommandMethod = type.GetMethod(nameof(SendCommand));
            this.sendQueryMethod = type.GetMethod(nameof(SendQuery));
            
            this.commandHandlerType = typeof(CommandHandler<>);
            this.queryHandlerType = typeof(QueryHandler<,>);

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

        private Task SendCommand<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand
        {
            var handler = serviceProvider.GetRequiredService<CommandHandler<TCommand>>();
            return handler.Handle(command, cancellationToken);
        }

        public Task<TResponse> SendQuery<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResponse>
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var handler = serviceProvider.GetRequiredService<QueryHandler<TQuery, TResponse>>();
            return handler.Handle(query, cancellationToken);
        }
    }
}