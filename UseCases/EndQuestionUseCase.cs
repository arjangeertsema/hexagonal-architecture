using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.DDD.Abstractions;
using Domain.Abstractions.Ports.Input;
using UseCases.Attributes;
using System.Threading;
using Common.CQRS.Abstractions.Attributes;
using Common.IAM.Abstractions.Queries;
using Domain.Abstractions;
using Common.CQRS.Abstractions.Commands;

namespace UseCases
{
    public class EndQuestionUseCaseHandler : ICommandHandler<EndQuestionUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore<IAnswerQuestionsAggregateRoot> aggregateRootStore;

        public EndQuestionUseCaseHandler(IMediator mediator, IAggregateRootStore<IAnswerQuestionsAggregateRoot> aggregateRootStore)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.aggregateRootStore = aggregateRootStore ?? throw new ArgumentNullException(nameof(aggregateRootStore));            
         }

        [HasPermission("a permission")]
        [Transactional]
        [MakeIdempotent]
        public async Task Handle(EndQuestionUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = await aggregateRootStore.Get(command.QuestionId, cancellationToken);

            var userId = await mediator.Ask(new GetUserId(), cancellationToken);

            aggregateRoot.EndQuestion
            (
                endedBy: userId
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