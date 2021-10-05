using System;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Domain.Abstractions.Ports.Input;
using UseCases.Attributes;
using Synion.CQRS.Abstractions.Ports;
using System.Threading;

namespace UseCases
{
    public class GetReviewAnswerTaskUseCaseHandler : IInputPortHandler<GetReviewAnswerTaskUseCase, GetReviewAnswerTaskUseCase.Response>
    {
        private readonly IMediator mediator;

        public GetReviewAnswerTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public Task<GetReviewAnswerTaskUseCase.Response> Handle(GetReviewAnswerTaskUseCase query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}