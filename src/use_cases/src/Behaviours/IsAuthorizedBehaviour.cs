using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.UseCases.Attributes;

namespace Reference.UseCases.Behaviours
{
    public class IsAuthorizedBehaviour : ICommandBehaviour, IQueryBehaviour
    {
        private readonly Type authorizeCommandType;
        private readonly Type authorizeQueryType;
        private readonly IMediator mediator;
        private readonly IServiceProvider serviceProvider;

        public IsAuthorizedBehaviour(
            IMediator mediator,
            IServiceProvider serviceProvider)
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            if (serviceProvider is null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            this.authorizeCommandType = typeof(IHasCommandAuthorization<>);
            this.authorizeQueryType = typeof(IHasQueryAuthorization<,>);
            this.mediator = mediator;
            this.serviceProvider = serviceProvider;
        }

        public async Task Handle(ICommand command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, CommandHandlerDelegate next)
        {
            var attr = attributeCollection.GetAttribute<IsAuthorizedAttribute>();
            if(attr == null)
            {
                await next();
                return;
            }

            if(!await IsAuthorized(command))
                throw new UnauthorizedAccessException();

            await next();
        }

        public async Task<object> Handle(IQuery<object> query, IAttributeCollection attributeCollection, CancellationToken cancellationToken, QueryHandlerDelegate<object> next)
        {
            var attr = attributeCollection.GetAttribute<IsAuthorizedAttribute>();
            if(attr == null)
            {
                return await next();;
            }

            var response = await next();

            if(!await IsAuthorized(query, response))
                throw new UnauthorizedAccessException();

            return response;
        }

        private async Task<bool> IsAuthorized(ICommand command)
        {
            var authorization = GetAuthorizeCommand(command);
            var identity = await GetIdentity();
            return await authorization.IsAuthorized(identity, command);
        }

        private async Task<bool> IsAuthorized(IQuery<object> query, object response)
        {
            var authorization = GetAuthorizeQuery(query, response);
            var identity = await GetIdentity();
            return await authorization.IsAuthorized(identity, query, response);
        }

        private IHasCommandAuthorization<ICommand> GetAuthorizeCommand(ICommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var type = this.authorizeCommandType.MakeGenericType(new Type[] { command.GetType() });
            return (IHasCommandAuthorization<ICommand>) this.serviceProvider.GetRequiredService(type);
        }

        private IHasQueryAuthorization<IQuery<object>, object> GetAuthorizeQuery(IQuery<object> query, object response)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var queryType = query.GetType();
            var queryParameterType = queryType.GenericTypeArguments[0];

            var type = this.authorizeQueryType.MakeGenericType(new Type[] { queryType, queryParameterType });
            return (IHasQueryAuthorization<IQuery<object>, object>) this.serviceProvider.GetRequiredService(type);
        }

        private async Task<string> GetIdentity()
        {
            var identity = await mediator.Send(new GetIdentityPort());
            if(identity == null)
                throw new UnauthorizedAccessException();

            return identity.Id;
        }
    }
}