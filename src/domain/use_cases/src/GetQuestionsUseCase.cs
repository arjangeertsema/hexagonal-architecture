using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.UseCases.Attributes;

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

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public async Task<Abstractions.Ports.Input.GetQuestionsUseCase.Response> Handle(Abstractions.Ports.Input.GetQuestionsUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}