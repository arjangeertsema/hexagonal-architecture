using System;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Synion.DDD.Abstractions;
using Domain.Abstractions.Ports.Input;
using Domain.Abstractions.Ports.Output;
using Domain.Core;
using UseCases.Attributes;
using Synion.CQRS.Abstractions.Ports;
using System.Threading;

namespace UseCases
{
    public class AnswerQuestionUseCaseHandler : IInputPortHandler<AnswerQuestionUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore<AnswerQuestionsAggregateRoot> aggregateRootStore;

        public AnswerQuestionUseCaseHandler(IMediator mediator, IAggregateRootStore<AnswerQuestionsAggregateRoot> aggregateRootStore)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.aggregateRootStore = aggregateRootStore ?? throw new ArgumentNullException(nameof(aggregateRootStore));           
        }

        [Transactional]
        [HasPermission("a permission")]
        [IsUserTaskOwner]
        [MakeIdempotent]
        public async Task Handle(AnswerQuestionUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = await aggregateRootStore.Get(command.QuestionId, cancellationToken);

            var identity = await mediator.Send(new GetIdentityPort(), cancellationToken);

            aggregateRoot.AnswerQuestion
            (
                taskId: command.UserTaskId, 
                answer: command.Answer, 
                answeredBy: identity.Id
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