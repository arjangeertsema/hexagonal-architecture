using System;
using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Commands;
using Common.IAM.Abstractions.Attributes;

namespace Common.IAM.Abstractions.Commands
{
    internal class IsAuthorizedCommandBehaviour<TCommand> : IsAuthorizedBehaviour, ICommandAttributeBehaviour<TCommand, IsAuthorizedAttribute>
        where TCommand : ICommand
    {
        private readonly ICommandAuthorization<TCommand> authorization;

        public IsAuthorizedCommandBehaviour(IMediator mediator, ICommandAuthorization<TCommand> authorization)
            : base(mediator)
        {
            this.authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
        }

        public async Task Handle(TCommand command, IsAuthorizedAttribute attribute, CancellationToken cancellationToken, CommandBehaviourDelegate next)
        {
            var userId = await GetUserId(cancellationToken);
            await authorization.Authorize(userId, command);
            await next();
        }
    }
}