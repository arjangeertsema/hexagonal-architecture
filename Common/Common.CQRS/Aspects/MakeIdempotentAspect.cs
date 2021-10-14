using System;
using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Aspects;
using Common.CQRS.Abstractions.Attributes;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Exceptions;
using Common.CQRS.Commands;

namespace Common.CQRS.Aspects
{
    public class MakeIdempotentCommandAspect<TCommand> : ICommandAttributeAspect<TCommand, MakeIdempotentAttribute>
        where TCommand : ICommand
    {
        private readonly IMediator mediator;

        public MakeIdempotentCommandAspect(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public async Task Handle(TCommand command, MakeIdempotentAttribute attribute, CancellationToken cancellationToken, CommandAspectDelegate next)
        {
            try
            {
                await this.mediator.Send(new RegisterCommand(command), cancellationToken);
                await next();
            }
            catch(CommandIsAlreadyHandledException)
            {
                //Stop pipeline, command has already been handled. Do not call next.
                return;
            }
            catch(DuplicateCommandIdException)
            {
                //Stop pipeline by throwing the exception;
                throw;
            }
        }
    }
}