using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.UseCases.Attributes;

namespace Reference.Domain.UseCases.Behaviours
{
    public class IsUserTaskOwnerBehaviour : ICommandBehaviour, IQueryBehaviour
    {
        private readonly IMediator mediator;

        public IsUserTaskOwnerBehaviour(
            IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(ICommand command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, CommandHandlerDelegate next)
        {
            var attr = attributeCollection.GetAttribute<IsUserTaskOwnerAttribute>();
            if(attr == null)
            {
                await next();
                return;
            }

            if(!await IsUserTaskOwner(command as IUserTask))
                throw new UnauthorizedAccessException();

            await next();
        }

        public async Task<object> Handle(IQuery<object> query, IAttributeCollection attributeCollection, CancellationToken cancellationToken, QueryHandlerDelegate<object> next)
        {
            var attr = attributeCollection.GetAttribute<IsUserTaskOwnerAttribute>();
            if(attr == null)
            {
                return await next();
            }
            
            if(!await IsUserTaskOwner(query as IUserTask))
                throw new UnauthorizedAccessException();

            return await next();
        }

        private async Task<bool> IsUserTaskOwner(IUserTask userTask)
        {
            if (userTask is null)
            {
                throw new ArgumentNullException(nameof(userTask));
            }

            var identity = await mediator.Send(new GetIdentityPort());
            if(identity == null)
                return false;

            return await mediator.Send(new IsUserTaskOwnerPort(identity: identity.Id, userTaskId: userTask.UserTaskId));
        }
    }
}