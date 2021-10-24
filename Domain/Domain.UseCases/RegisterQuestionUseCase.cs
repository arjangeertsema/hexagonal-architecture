using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.UseCases;
using System.Threading;
using Common.CQRS.Abstractions.Attributes;
using Domain.Abstractions;
using Common.IAM.Abstractions.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Common.DDD.Abstractions.Commands;

namespace Domain.UseCases
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class RegisterQuestionUseCaseHandler : ICommandHandler<RegisterQuestionUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAnswerQuestionsAggregateRootFactory aggregateRootFactory;

        public RegisterQuestionUseCaseHandler(IMediator mediator, IAnswerQuestionsAggregateRootFactory aggregateRootFactory)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.aggregateRootFactory = aggregateRootFactory ?? throw new ArgumentNullException(nameof(aggregateRootFactory));
        }

        [HasPermission("ASK_QUESTION")]
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

            await mediator.Send(SaveAggregateRootFactory.Create(command.CommandId, aggregateRoot), cancellationToken);
        }
    }
}
