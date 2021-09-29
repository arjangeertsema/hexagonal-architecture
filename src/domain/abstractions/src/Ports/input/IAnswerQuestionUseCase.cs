using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public interface IAnswerQuestionUseCase : IInputPort<IAnswerQuestionUseCase.Command>
    {
        class Command : ICommand
        {
            public Guid CommandId => throw new NotImplementedException();
        }
    }
}
