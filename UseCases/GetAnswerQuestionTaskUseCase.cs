using System;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Domain.Abstractions.Ports.Input;
using UseCases.Attributes;
using Synion.CQRS.Abstractions.Ports;
using System.Threading;

namespace UseCases
{
    public class GetAnswerQuestionTaskUseCaseHandler : IInputPortHandler<GetAnswerQuestionTaskUseCase, GetAnswerQuestionTaskUseCase.Response>
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
