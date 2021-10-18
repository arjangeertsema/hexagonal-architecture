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
using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions.Attributes;

namespace Domain.UseCases
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class GetAnswerQuestionTaskUseCaseHandler : IQueryHandler<GetAnswerQuestionTaskUseCase, GetAnswerQuestionTaskUseCase.Response>
    {
        private readonly IMediator mediator;

        public GetAnswerQuestionTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("ANSWER_QUESTION")]
        [IsUserTaskOwner]
        public async Task<GetAnswerQuestionTaskUseCase.Response> Handle(GetAnswerQuestionTaskUseCase query, CancellationToken cancellationToken)
        {
            var task = await mediator.Ask(new GetUserTask(query.UserTaskId));
            var questionId = Guid.Parse(task.ReferenceId);            
            var instance = await mediator.Ask(new GetAnswerQuestionsInstance(questionId));

            return Map(task, instance);            
        }
        
        private GetAnswerQuestionTaskUseCase.Response Map(GetUserTask.Response task, object instance)
        {
            throw new NotImplementedException();
        }
    }
}
