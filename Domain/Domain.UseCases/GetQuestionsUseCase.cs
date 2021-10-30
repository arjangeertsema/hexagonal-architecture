using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.UseCases;
using Common.CQRS.Abstractions.Queries;
using System.Threading;
using Common.IAM.Abstractions.Attributes;
using Domain.Abstractions.Ports;
using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions.Attributes;

namespace Domain.UseCases
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class GetQuestionsUseCaseHandler : IQueryHandler<GetQuestionsUseCase, GetQuestionsUseCase.Response>
    {
        private readonly IMediator mediator;

        public GetQuestionsUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("GET_QUESTION")]
        [IsAuthorized]
        public async Task<GetQuestionsUseCase.Response> Handle(GetQuestionsUseCase query, CancellationToken cancellationToken)
        {
            var instance = await mediator.Ask(new GetAnswerQuestionsPort(query.Limit, query.Offset));

            return Map(instance);
        }

        private GetQuestionsUseCase.Response Map(GetAnswerQuestionsPort.Response instance)
        {
            throw new NotImplementedException();
        }
    }
}