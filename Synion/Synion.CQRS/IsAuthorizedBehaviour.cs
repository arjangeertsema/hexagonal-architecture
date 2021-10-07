using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS.Abstractions;

namespace Synion.CQRS
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    internal abstract class IsAuthorizedBehaviour
    {
        protected readonly IMediator mediator;
        private readonly IPrincipal principal;

        public IsAuthorizedBehaviour(IMediator mediator, IPrincipal principal)
        {
            this.mediator = mediator ??  throw new ArgumentNullException(nameof(mediator));
            this.principal = principal;
        }

        protected async Task<string> GetIdentity(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}