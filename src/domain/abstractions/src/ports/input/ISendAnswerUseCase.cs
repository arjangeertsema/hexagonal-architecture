using System;

namespace example.domain.abstractions.ports.input
{
    public interface ISendAnswerUseCase : IInputPort<ISendAnswerUseCase.Command>
    {
        public class Command : ICommand
        {
            public Guid CommandId => throw new NotImplementedException();
        }
    }
}
