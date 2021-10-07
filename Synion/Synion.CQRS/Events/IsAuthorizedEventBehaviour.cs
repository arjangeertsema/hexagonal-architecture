using System;
using System.Threading;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Events;
using Synion.CQRS.Abstractions.Attributes;
using System.Security.Principal;

namespace Synion.CQRS.Events
{
    internal class IsAuthorizedEventBehaviour<TEvent> : IsAuthorizedBehaviour, IEventAttributeBehaviour<TEvent, IsAuthorizedAttribute>
        where TEvent : IEvent
    {
        private readonly IEventAuthorization<TEvent> authorization;

        public IsAuthorizedEventBehaviour(IMediator mediator, IPrincipal principal, IEventAuthorization<TEvent> authorization)
            : base(mediator, principal)
        {
            this.authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
        }

        public async Task Handle(TEvent @event, IsAuthorizedAttribute attribute, CancellationToken cancellationToken, EventBehaviourDelegate next)
        {
            var identity = await GetIdentity(cancellationToken);
            if(!await authorization.IsAuthorized(identity, @event))
                throw new UnauthorizedAccessException();

            await next();
        }
    }
}