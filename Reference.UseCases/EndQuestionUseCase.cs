using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.DDD;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.Core;
using Reference.UseCases.Attributes;

namespace Reference.UseCases
{
    public class EndQuestionUseCaseHandler :IInputPortHandler<EndQuestionUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore aggregateRootStore;

        public EndQuestionUseCaseHandler(IMediator mediator, IAggregateRootStore aggregateRootStore)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.aggregateRootStore = aggregateRootStore ?? throw new ArgumentNullException(nameof(aggregateRootStore));            
         }

        [Transactional]
        [HasPermission("a permission")]
        [MakeIdempotent]
        public async Task Handle(EndQuestionUseCase command)
        {
            var aggregateRoot = await aggregateRootStore.Get<AnswerQuestionsAggregateRoot>(command.QuestionId);

            var identity = await mediator.Send(new GetIdentityPort());

            aggregateRoot.EndQuestion
            (
                endedBy: identity.Id
            );

            await aggregateRootStore.Save
            (
                commandId: command.CommandId, 
                aggregateRoot: aggregateRoot
            );
        }
    }
}