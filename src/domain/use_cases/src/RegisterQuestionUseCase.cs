using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.DDD;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.Domain.Core;
using Reference.Domain.UseCases.Attributes;

namespace Reference.Domain.UseCases
{
    public class RegisterQuestionUseCase :IInputPortHandler<Abstractions.Ports.Input.RegisterQuestionUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore aggregateRootStore;

        public RegisterQuestionUseCase(
            IMediator mediator,
            IAggregateRootStore aggregateRootStore
        )
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            if (aggregateRootStore is null)
            {
                throw new ArgumentNullException(nameof(aggregateRootStore));
            }

            this.mediator = mediator;
            this.aggregateRootStore = aggregateRootStore;         
         }

        [Transactional]
        [HasPermission("a permission")]
        [MakeIdempotent]
        public async Task Handle(Abstractions.Ports.Input.RegisterQuestionUseCase command)
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
