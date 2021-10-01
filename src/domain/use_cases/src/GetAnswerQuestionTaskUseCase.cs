using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;

namespace Reference.Domain.UseCases
{
    public class GetAnswerQuestionTaskUseCase : IInputPort<Abstractions.Ports.Input.GetAnswerQuestionTaskUseCase>
    {
        private readonly IMediator mediator;

        public GetAnswerQuestionTaskUseCase(
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
        public async Task<Abstractions.Ports.Input.GetAnswerQuestionTaskUseCase.Response> Handle(Abstractions.Ports.Input.GetAnswerQuestionTaskUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}
