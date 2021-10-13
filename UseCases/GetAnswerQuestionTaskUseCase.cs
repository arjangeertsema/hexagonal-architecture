using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.Ports.Input;
using UseCases.Attributes;
using Common.CQRS.Abstractions.Queries;
using System.Threading;

namespace UseCases
{
    public class GetAnswerQuestionTaskUseCaseHandler : IQueryHandler<GetAnswerQuestionTaskUseCase, GetAnswerQuestionTaskUseCase.Response>
    {
        private readonly IMediator mediator;

        public GetAnswerQuestionTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public Task<GetAnswerQuestionTaskUseCase.Response> Handle(GetAnswerQuestionTaskUseCase query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
