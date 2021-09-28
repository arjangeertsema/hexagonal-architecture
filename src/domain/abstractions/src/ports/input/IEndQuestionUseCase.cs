using System;

namespace example.domain.abstractions.ports.input
{
    public interface IEndQuestionUseCase : IInputPort<IEndQuestionUseCase.Command> 
    {
        public class Command : ICommand
        {
            public Guid CommandId => throw new NotImplementedException();
        }
    }
}