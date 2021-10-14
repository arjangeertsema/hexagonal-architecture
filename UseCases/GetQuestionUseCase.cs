using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.UseCases;
using Common.CQRS.Abstractions.Queries;
using System.Threading;
using Common.IAM.Abstractions.Attributes;

namespace UseCases
{
    public class GetQuestionUseCaseHandler : IQueryHandler<GetQuestionUseCase, GetQuestionUseCase.Response>
    {
        private readonly IMediator mediator;

        public GetQuestionUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("a permission")]
        [IsAuthorized]
        public Task<GetQuestionUseCase.Response> Handle(GetQuestionUseCase query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}