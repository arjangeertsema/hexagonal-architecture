using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;

namespace Reference.Domain.UseCases
{
    public class GetModifyAnswerTaskUseCase : IInputPort<Abstractions.Ports.Input.GetModifyAnswerTaskUseCase>
    {
        private readonly IMediator mediator;

        public GetModifyAnswerTaskUseCase(
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
        public async Task<Abstractions.Ports.Input.GetModifyAnswerTaskUseCase.Response> Handle(Abstractions.Ports.Input.GetModifyAnswerTaskUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}
