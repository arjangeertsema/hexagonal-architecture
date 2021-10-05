using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.DDD;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.Domain.Core;
using Reference.UseCases.Attributes;

namespace Reference.UseCases
{
    public class RegisterQuestionUseCaseHandler :IInputPortHandler<RegisterQuestionUseCase>
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
        public async Task Handle(RegisterQuestionUseCase command)
        {
            var aggregateRoot = AnswerQuestionsAggregateRoot.Start
            (
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
