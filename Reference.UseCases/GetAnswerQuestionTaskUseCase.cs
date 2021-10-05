using System;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.UseCases.Attributes;
using Synion.CQRS.Abstractions.Ports;

namespace Reference.UseCases
{
    public class GetAnswerQuestionTaskUseCaseHandler : IInputPort<GetAnswerQuestionTaskUseCase>
    {
        private readonly IMediator mediator;

        public GetAnswerQuestionTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public Task<GetAnswerQuestionTaskUseCase.Response> Handle(GetAnswerQuestionTaskUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}
