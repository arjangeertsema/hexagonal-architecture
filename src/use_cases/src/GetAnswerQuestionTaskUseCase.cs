using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.UseCases.Attributes;

namespace Reference.UseCases
{
    public class GetAnswerQuestionTaskUseCaseHandler : IInputPort<GetAnswerQuestionTaskUseCase>
    {
        private readonly IMediator mediator;

        public GetAnswerQuestionTaskUseCaseHandler(IMediator mediator)
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.mediator = mediator;
        }

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public async Task<GetAnswerQuestionTaskUseCase.Response> Handle(GetAnswerQuestionTaskUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}
