using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public interface IEndQuestionUseCase : IInputPort<IEndQuestionUseCase.Command> 
    {
        public class Command : ICommand
        {
            public Guid CommandId => throw new NotImplementedException();
        }
    }
}