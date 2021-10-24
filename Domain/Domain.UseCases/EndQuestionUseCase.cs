using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.UseCases;
using System.Threading;
using Common.CQRS.Abstractions.Attributes;
using Common.IAM.Abstractions.Queries;
using Domain.Abstractions;
using Common.IAM.Abstractions.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Common.DDD.Abstractions.Commands;
using Common.DDD.Abstractions.Queries;

namespace Domain.UseCases
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class EndQuestionUseCaseHandler : ICommandHandler<EndQuestionUseCase>
    {
        private readonly IMediator mediator;

        public EndQuestionUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("END_QUESTION")]
        [Transactional]
        [MakeIdempotent]
        public async Task Handle(EndQuestionUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot>(command.QuestionId), cancellationToken);

            var userId = await mediator.Ask(new GetUserId(), cancellationToken);

            aggregateRoot.EndQuestion
            (
                endedBy: userId
            );

            await mediator.Send(SaveAggregateRootFactory.Create(command.CommandId, aggregateRoot), cancellationToken);
        }
    }
}