using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.UseCases.Attributes;

namespace Reference.UseCases
{
    public class GetReviewAnswerTaskUseCaseHandler : IInputPort<GetReviewAnswerTaskUseCase>
    {
        private readonly IMediator mediator;

        public GetReviewAnswerTaskUseCaseHandler(IMediator mediator)
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.mediator = mediator;
        }

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public async Task<GetReviewAnswerTaskUseCase.Response> Handle(GetReviewAnswerTaskUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}