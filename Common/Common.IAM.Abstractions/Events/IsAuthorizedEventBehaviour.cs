using System;
using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Events;
using Common.IAM.Abstractions.Attributes;

namespace Common.IAM.Abstractions.Events
{
    internal class IsAuthorizedEventBehaviour<TEvent> : IsAuthorizedBehaviour, IEventAttributeBehaviour<TEvent, IsAuthorizedAttribute>
        where TEvent : IEvent
    {
        private readonly IEventAuthorization<TEvent> authorization;

        public IsAuthorizedEventBehaviour(IMediator mediator, IEventAuthorization<TEvent> authorization)
            : base(mediator)
        {
            this.authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
        }

        public async Task Handle(TEvent @event, IsAuthorizedAttribute attribute, CancellationToken cancellationToken, EventBehaviourDelegate next)
        {
            var userId = await GetUserId(cancellationToken);
            await authorization.Authorize(userId, @event);
            await next();
        }
    }
}