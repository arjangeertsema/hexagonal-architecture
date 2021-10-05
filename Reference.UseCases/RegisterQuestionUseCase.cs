using System;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Synion.DDD.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.Domain.Core;
using Reference.UseCases.Attributes;
using Synion.CQRS.Abstractions.Ports;
using System.Threading;

namespace Reference.UseCases
{
    public class RegisterQuestionUseCaseHandler : IInputPortHandler<RegisterQuestionUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore aggregateRootStore;

        public RegisterQuestionUseCaseHandler(IMediator mediator, IAggregateRootStore aggregateRootStore)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.aggregateRootStore = aggregateRootStore ?? throw new ArgumentNullException(nameof(aggregateRootStore));            
         }

        [Transactional]
        [HasPermission("a permission")]
        [MakeIdempotent]
        public async Task Handle(RegisterQuestionUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = AnswerQuestionsAggregateRoot.Start
            (
                questionId: command.QuestionId,
                subject: command.Subject,
                question: command.Question,
                askedBy: command.AskedBy
            );

            await aggregateRootStore.Save
            (
                commandId: command.CommandId, 
                aggregateRoot: aggregateRoot
            );
        }
    }
}
