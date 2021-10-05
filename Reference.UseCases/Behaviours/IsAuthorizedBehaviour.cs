using System;
using System.Threading;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.UseCases.Attributes;
using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Queries;

namespace Reference.UseCases.Behaviours
{
    public abstract class IsAuthorizedBehaviour
    {
        protected readonly IMediator mediator;

        public IsAuthorizedBehaviour(IMediator mediator)
        {
            this.mediator = mediator ??  throw new ArgumentNullException(nameof(mediator));
        }

        protected async Task<string> GetIdentity()
        {
            var identity = await mediator.Send(new GetIdentityPort());
            if(identity == null)
                throw new UnauthorizedAccessException();

            return identity.Id;
        }
    }

    public class IsAuthorizedCommandBehaviour<TCommand> : IsAuthorizedBehaviour, ICommandAttributeBehaviour<TCommand, IsAuthorizedAttribute>
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
            var identity = await GetIdentity();
            if(!await authorization.IsAuthorized(identity, command))
                throw new UnauthorizedAccessException();

            await next();
        }
    }

    public class IsAuthorizedQueryBehaviour<TQuery, TResponse> : IsAuthorizedBehaviour, IQueryAttributeBehaviour<TQuery, TResponse, IsAuthorizedAttribute>
        where TQuery : IQuery<TResponse>
    {
        private readonly IQueryAuthorization<TQuery, TResponse> authorization;

        public IsAuthorizedQueryBehaviour(IMediator mediator, IQueryAuthorization<TQuery, TResponse> authorization)
            : base(mediator)
        {
            this.authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
        }

        public async Task<TResponse> Handle(TQuery query, IsAuthorizedAttribute attribute, CancellationToken cancellationToken, QueryBehaviourDelegate<TResponse> next)
        {
            var response = await next();

            var identity = await GetIdentity();
            if(!await authorization.IsAuthorized(identity, query, response))
                throw new UnauthorizedAccessException();

            return response;
        }      
    }
}