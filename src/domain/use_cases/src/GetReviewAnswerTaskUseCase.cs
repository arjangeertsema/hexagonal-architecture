using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;

namespace Reference.Domain.UseCases
{
    public class GetReviewAnswerTaskUseCase : IInputPort<Abstractions.Ports.Input.GetReviewAnswerTaskUseCase>
    {
        private readonly IMediator mediator;

        public GetReviewAnswerTaskUseCase(
            IMediator mediator
        )
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.mediator = mediator;
        }

        [PreAuthorize("a permission")]
        public async Task<Abstractions.Ports.Input.GetReviewAnswerTaskUseCase.Response> Handle(Abstractions.Ports.Input.GetReviewAnswerTaskUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}