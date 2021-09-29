using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public interface IModifyAnswerUseCase : IInputPort<IModifyAnswerUseCase.Command>
    {
        public class Command : ICommand
        {
            public Guid CommandId => throw new NotImplementedException();
        }
    }
}
