using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.UseCases.Attributes;

namespace Reference.UseCases
{
    public class GetQuestionsUseCaseHandler : IInputPort<GetQuestionsUseCase>
    {
        private readonly IMediator mediator;

        public GetQuestionsUseCaseHandler(IMediator mediator)
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.mediator = mediator;
        }

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public async Task<GetQuestionsUseCase.Response> Handle(GetQuestionsUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}