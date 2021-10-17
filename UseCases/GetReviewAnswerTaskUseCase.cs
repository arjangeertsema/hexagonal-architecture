using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.UseCases;
using Common.CQRS.Abstractions.Queries;
using System.Threading;
using Common.IAM.Abstractions.Attributes;
using Common.UserTasks.Abstractions.Attributes;
using Common.UserTasks.Abstractions.Queries;
using Domain.Abstractions.Ports;

namespace UseCases
{
    public class GetReviewAnswerTaskUseCaseHandler : IQueryHandler<GetReviewAnswerTaskUseCase, GetReviewAnswerTaskUseCase.Response>
    {
        private readonly IMediator mediator;

        public GetReviewAnswerTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("REVIEW_ANSWER")]
        [IsUserTaskOwner]
        public async Task<GetReviewAnswerTaskUseCase.Response> Handle(GetReviewAnswerTaskUseCase query, CancellationToken cancellationToken)
        {
            var task = await mediator.Ask(new GetUserTask(query.UserTaskId));
            var questionId = Guid.Parse(task.ReferenceId);
            var instance = await mediator.Ask(new GetAnswerQuestionsInstance(questionId));

            return Map(task, instance);
        }

        private GetReviewAnswerTaskUseCase.Response Map(GetUserTask.Response task, GetAnswerQuestionsInstance.Response instance)
        {
            throw new NotImplementedException();
        }
    }
}