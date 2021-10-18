using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.DDD.Abstractions;
using Domain.Abstractions.UseCases;
using System.Threading;
using Common.CQRS.Abstractions.Attributes;
using Domain.Abstractions;
using Common.IAM.Abstractions.Queries;
using Common.IAM.Abstractions.Attributes;
using Common.UserTasks.Abstractions.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.UseCases
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class ModifyAnswerUseCaseHandler : ICommandHandler<ModifyAnswerUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore<IAnswerQuestionsAggregateRoot> aggregateRootStore;

        public ModifyAnswerUseCaseHandler(IMediator mediator, IAggregateRootStore<IAnswerQuestionsAggregateRoot> aggregateRootStore)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.aggregateRootStore = aggregateRootStore ?? throw new ArgumentNullException(nameof(aggregateRootStore));           
        }

        [HasPermission("ANSWER_QUESTION")]
        [IsUserTaskOwner]
        [Transactional]
        [MakeIdempotent]
        public async Task Handle(ModifyAnswerUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = await aggregateRootStore.Get(command.QuestionId, cancellationToken);

            var userId = await mediator.Ask(new GetUserId(), cancellationToken);

            aggregateRoot.ModifyAnswer
            (
                userTaskId: command.UserTaskId, 
                answer: command.Answer, 
                modifiedBy: userId
            );

            await aggregateRootStore.Save
            (
                commandId: command.CommandId, 
                aggregateRoot: aggregateRoot,
                cancellationToken: cancellationToken
            );
        }
    }
}