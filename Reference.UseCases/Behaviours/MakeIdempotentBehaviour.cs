using System;
using System.Threading;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.Abstractions.Ports.Output.Exceptions;
using Reference.UseCases.Attributes;
using Synion.CQRS.Abstractions.Commands;

namespace Reference.UseCases.Behaviours
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
                await this.mediator.Send(new RegisterCommandPort(command));
                await next();
            }
            catch(CommandAlreadyExistsException)
            {
                //Stop pipeline, command has already been handled. Do not call next.
                return;
            }
        }
    }
}