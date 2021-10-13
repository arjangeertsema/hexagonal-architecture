using System;
using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Queries;
using Common.IAM.Abstractions.Attributes;

namespace Common.IAM.Abstractions.Queries
{
    internal class IsAuthorizedQueryBehaviour<TQuery, TResponse> : IsAuthorizedBehaviour, IQueryAttributeBehaviour<TQuery, TResponse, IsAuthorizedAttribute>
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

            var userId = await GetUserId(cancellationToken);
            await authorization.Authorize(userId, query, response);
            return response;
        }      
    }
}