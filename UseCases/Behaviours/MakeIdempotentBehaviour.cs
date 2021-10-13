using System;
using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.Ports.Output;
using Domain.Abstractions.Ports.Output.Exceptions;
using UseCases.Attributes;
using Common.CQRS.Abstractions.Commands;

namespace UseCases.Behaviours
{
    public class MakeIdempotentCommandBehaviour<TCommand> : ICommandAttributeBehaviour<TCommand, MakeIdempotentAttribute>
        where TCommand : ICommand
    {
        private readonly IMediator mediator;

        public MakeIdempotentCommandBehaviour(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public async Task Handle(TCommand command, MakeIdempotentAttribute attribute, CancellationToken cancellationToken, CommandBehaviourDelegate next)
        {
            try
            {
                await this.mediator.Send(new RegisterCommandPort(command), cancellationToken);
                await next();
            }
            catch(CommandIsAlreadyHandledException)
            {
                //Stop pipeline, command has already been handled. Do not call next.
                return;
            }
        }
    }
}