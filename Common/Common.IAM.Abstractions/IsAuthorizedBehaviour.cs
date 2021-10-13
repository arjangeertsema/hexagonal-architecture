using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Attributes;
using Common.IAM.Abstractions.Queries;

namespace Common.IAM.Abstractions
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    internal abstract class IsAuthorizedBehaviour
    {
        protected readonly IMediator mediator;

        public IsAuthorizedBehaviour(IMediator mediator)
        {
            this.mediator = mediator ??  throw new ArgumentNullException(nameof(mediator));
        }

        protected Task<string> GetUserId(CancellationToken cancellationToken)
        {
            return mediator.Ask(new GetUserId(), cancellationToken);
        }
    }
}