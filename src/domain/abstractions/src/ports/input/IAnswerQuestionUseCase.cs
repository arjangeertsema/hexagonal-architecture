using System;

namespace example.domain.abstractions.ports.input
{
    public interface IAnswerQuestionUseCase : IInputPort<IAnswerQuestionUseCase.Command>
    {
        class Command : ICommand
        {
            public Guid CommandId => throw new NotImplementedException();
        }
    }
}
