using System;
using System.Threading.Tasks;
using System.Transactions;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.DDD;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.Abstractions.Ports.Output.Exceptions;
using Reference.Domain.Core;

namespace Reference.Domain.UseCases
{
    public class AnswerQuestionUseCase : IInputPortHandler<Abstractions.Ports.Input.AnswerQuestionUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore aggregateRootStore;

        public AnswerQuestionUseCase(
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

        [Scoped]
        [PreAuthorize("a permission")]
        [MakeIdempotent]
        public async Task Handle(Abstractions.Ports.Input.AnswerQuestionUseCase command)
        {
            var aggregateRoot = await aggregateRootStore.Get<AnswerQuestionsAggregateRoot>(command.QuestionId);

            var identity = await mediator.Send(new GetIdentity());

            aggregateRoot.AnswerQuestion
            (
                taskId: command.TaskId, 
                answer: command.Answer, 
                answeredBy: identity.Id
            );

            await aggregateRootStore.Save
            (
                commandId: command.CommandId, 
                aggregateRoot: aggregateRoot
            );
        }
    }
}