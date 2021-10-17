using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.UseCases;
using Common.CQRS.Abstractions.Queries;
using System.Threading;
using Common.IAM.Abstractions.Attributes;
using Domain.Abstractions.Ports;

namespace UseCases
{
    public class GetQuestionUseCaseHandler : IQueryHandler<GetQuestionUseCase, GetQuestionUseCase.Response>
    {
        private readonly IMediator mediator;

        public GetQuestionUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("GET_QUESTION")]
        [IsAuthorized]
        public async Task<GetQuestionUseCase.Response> Handle(GetQuestionUseCase query, CancellationToken cancellationToken)
        {
            var instance = await mediator.Ask(new GetAnswerQuestionsInstance(query.QuestionId));

            return Map(instance);
        }

        private GetQuestionUseCase.Response Map(GetAnswerQuestionsInstance.Response instance)
        {
            throw new NotImplementedException();
        }
    }
}