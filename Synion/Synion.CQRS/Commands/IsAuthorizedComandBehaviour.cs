using System;
using System.Threading;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Attributes;
using System.Security.Principal;

namespace Synion.CQRS.Commands
{
    internal class IsAuthorizedCommandBehaviour<TCommand> : IsAuthorizedBehaviour, ICommandAttributeBehaviour<TCommand, IsAuthorizedAttribute>
        where TCommand : ICommand
    {
        private readonly ICommandAuthorization<TCommand> authorization;

        public IsAuthorizedCommandBehaviour(IMediator mediator, IPrincipal principal, ICommandAuthorization<TCommand> authorization)
            : base(mediator, principal)
        {
            this.authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
        }

        public async Task Handle(TCommand command, IsAuthorizedAttribute attribute, CancellationToken cancellationToken, CommandBehaviourDelegate next)
        {
            var identity = await GetIdentity(cancellationToken);
            if(!await authorization.IsAuthorized(identity, command))
                throw new UnauthorizedAccessException();

            await next();
        }
    }
}