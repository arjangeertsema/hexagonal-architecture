using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;

namespace Reference.Domain.UseCases
{
    public class GetQuestionsUseCase : IInputPort<Abstractions.Ports.Input.GetQuestionsUseCase>
    {
        private readonly IMediator mediator;

        public GetQuestionsUseCase(
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
        public async Task<Abstractions.Ports.Input.GetQuestionsUseCase.Response> Handle(Abstractions.Ports.Input.GetQuestionsUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}