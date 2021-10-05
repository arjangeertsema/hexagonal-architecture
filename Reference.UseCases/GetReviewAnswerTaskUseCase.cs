using System;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.UseCases.Attributes;
using Synion.CQRS.Abstractions.Ports;

namespace Reference.UseCases
{
    public class GetReviewAnswerTaskUseCaseHandler : IInputPort<GetReviewAnswerTaskUseCase>
    {
        private readonly IMediator mediator;

        public GetReviewAnswerTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public Task<GetReviewAnswerTaskUseCase.Response> Handle(GetReviewAnswerTaskUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}