using System;
using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Aspects;
using Common.CQRS.Abstractions.Attributes;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Events;
using Common.CQRS.Abstractions.Queries;
using Common.IAM.Abstractions.Attributes;
using Common.IAM.Abstractions.Commands;
using Common.IAM.Abstractions.Events;
using Common.IAM.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Common.IAM.Abstractions.Aspects
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public abstract class IsAuthorizedAspect
    {
        protected readonly IMediator mediator;

        public IsAuthorizedAspect(IMediator mediator)
        {
            this.mediator = mediator ??  throw new ArgumentNullException(nameof(mediator));
        }

        protected Task<string> GetUserId(CancellationToken cancellationToken)
        {
            return mediator.Ask(new GetUserId(), cancellationToken);
        }
    }

    public class IsAuthorizedCommandAspect<TCommand> : IsAuthorizedAspect, ICommandAttributeAspect<TCommand, IsAuthorizedAttribute>
        where TCommand : ICommand
    {
        private readonly ICommandAuthorization<TCommand> authorization;

        public IsAuthorizedCommandAspect(IMediator mediator, ICommandAuthorization<TCommand> authorization)
            : base(mediator)
        {
            this.authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
        }

        public async Task Handle(TCommand command, IsAuthorizedAttribute attribute, CancellationToken cancellationToken, CommandAspectDelegate next)
        {
            var userId = await GetUserId(cancellationToken);
            await authorization.Authorize(userId, command);
            await next();
        }
    }
    
    public class IsAuthorizedEventAspect<TEvent> : IsAuthorizedAspect, IEventAttributeAspect<TEvent, IsAuthorizedAttribute>
        where TEvent : IEvent
    {
        private readonly IEventAuthorization<TEvent> authorization;

        public IsAuthorizedEventAspect(IMediator mediator, IEventAuthorization<TEvent> authorization)
            : base(mediator)
        {
            this.authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
        }

        public async Task Handle(TEvent @event, IsAuthorizedAttribute attribute, CancellationToken cancellationToken, EventAspectDelegate next)
        {
            var userId = await GetUserId(cancellationToken);
            await authorization.Authorize(userId, @event);
            await next();
        }
    }
    public class IsAuthorizedQueryAspect<TQuery, TResponse> : IsAuthorizedAspect, IQueryAttributeAspect<TQuery, TResponse, IsAuthorizedAttribute>
        where TQuery : IQuery<TResponse>
    {
        private readonly IQueryAuthorization<TQuery, TResponse> authorization;

        public IsAuthorizedQueryAspect(IMediator mediator, IQueryAuthorization<TQuery, TResponse> authorization)
            : base(mediator)
        {
            this.authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
        }

        public async Task<TResponse> Handle(TQuery query, IsAuthorizedAttribute attribute, CancellationToken cancellationToken, QueryAspectDelegate<TResponse> next)
        {
            var response = await next();
            var userId = await GetUserId(cancellationToken);
            await authorization.Authorize(userId, query, response);
            return response;
        }      
    }
}