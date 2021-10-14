using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.DDD.Abstractions;
using Domain.Abstractions.UseCases;
using System.Threading;
using Common.CQRS.Abstractions.Attributes;
using Domain.Abstractions;
using Common.IAM.Abstractions.Queries;
using Common.CQRS.Abstractions.Commands;
using Common.IAM.Abstractions.Attributes;
using Common.UserTasks.Abstractions.Attributes;

namespace UseCases
{
    public class AnswerQuestionUseCaseHandler : ICommandHandler<AnswerQuestionUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore<IAnswerQuestionsAggregateRoot> aggregateRootStore;

        public AnswerQuestionUseCaseHandler(IMediator mediator, IAggregateRootStore<IAnswerQuestionsAggregateRoot> aggregateRootStore)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.aggregateRootStore = aggregateRootStore ?? throw new ArgumentNullException(nameof(aggregateRootStore));           
        }

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        [Transactional]
        [MakeIdempotent]
        public async Task Handle(AnswerQuestionUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = await aggregateRootStore.Get(command.QuestionId, cancellationToken);

            var userId = await mediator.Ask(new GetUserId(), cancellationToken);

            aggregateRoot.AnswerQuestion
            (
                userTaskId: command.UserTaskId, 
                answer: command.Answer, 
                answeredBy: userId
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