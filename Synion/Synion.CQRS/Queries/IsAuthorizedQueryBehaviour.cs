using System;
using System.Threading;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Queries;
using Synion.CQRS.Abstractions.Attributes;
using System.Security.Principal;

namespace Synion.CQRS.Queries
{
    internal class IsAuthorizedQueryBehaviour<TQuery, TResponse> : IsAuthorizedBehaviour, IQueryAttributeBehaviour<TQuery, TResponse, IsAuthorizedAttribute>
        where TQuery : IQuery<TResponse>
    {
        private readonly IQueryAuthorization<TQuery, TResponse> authorization;

        public IsAuthorizedQueryBehaviour(IMediator mediator, IPrincipal principal, IQueryAuthorization<TQuery, TResponse> authorization)
            : base(mediator, principal)
        {
            this.authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
        }

        public async Task<TResponse> Handle(TQuery query, IsAuthorizedAttribute attribute, CancellationToken cancellationToken, QueryBehaviourDelegate<TResponse> next)
        {
            var response = await next();

            var identity = await GetIdentity(cancellationToken);
            if(!await authorization.IsAuthorized(identity, query, response))
                throw new UnauthorizedAccessException();

            return response;
        }      
    }
}