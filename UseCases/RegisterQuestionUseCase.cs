using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.DDD.Abstractions;
using Domain.Abstractions.Ports.Input;
using Common.CQRS.Abstractions.Commands;
using System.Threading;
using Common.CQRS.Abstractions.Attributes;
using Domain.Abstractions;
using Common.IAM.Abstractions.Attributes;

namespace UseCases
{
    public class RegisterQuestionUseCaseHandler : ICommandHandler<RegisterQuestionUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore<IAnswerQuestionsAggregateRoot> aggregateRootStore;
        private readonly IAnswerQuestionsAggregateRootFactory aggregateRootFactory;

        public RegisterQuestionUseCaseHandler(IMediator mediator, IAggregateRootStore<IAnswerQuestionsAggregateRoot> aggregateRootStore, IAnswerQuestionsAggregateRootFactory aggregateRootFactory)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.aggregateRootStore = aggregateRootStore ?? throw new ArgumentNullException(nameof(aggregateRootStore));
            this.aggregateRootFactory = aggregateRootFactory ?? throw new ArgumentNullException(nameof(aggregateRootFactory));
        }

        [HasPermission("a permission")]
        [Transactional]
        [MakeIdempotent]
        public async Task Handle(RegisterQuestionUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = this.aggregateRootFactory.Create
            (
                questionId: command.QuestionId,
                subject: command.Subject,
                question: command.Question,
                askedBy: command.AskedBy
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
