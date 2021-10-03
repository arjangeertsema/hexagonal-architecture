using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.UseCases.Attributes;

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

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public async Task<Abstractions.Ports.Input.GetReviewAnswerTaskUseCase.Response> Handle(Abstractions.Ports.Input.GetReviewAnswerTaskUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}