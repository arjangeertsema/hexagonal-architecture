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
    public class GetQuestionsUseCaseHandler : IQueryHandler<GetQuestionsUseCase, GetQuestionsUseCase.Response>
    {
        private readonly IMediator mediator;

        public GetQuestionsUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("GET_QUESTION")]
        [IsAuthorized]
        public async Task<GetQuestionsUseCase.Response> Handle(GetQuestionsUseCase query, CancellationToken cancellationToken)
        {
            var instance = await mediator.Ask(new GetAnswerQuestionsInstances(query.Limit, query.Offset));

            return Map(instance);
        }

        private GetQuestionsUseCase.Response Map(GetAnswerQuestionsInstances.Response instance)
        {
            throw new NotImplementedException();
        }
    }
}