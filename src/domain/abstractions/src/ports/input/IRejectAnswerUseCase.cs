using System;

namespace example.domain.abstractions.ports.input
{
    public interface IRejectAnswerUseCase : IInputPort<IRejectAnswerUseCase.Command>
    {
        public class Command : ICommand
        {
            public Guid CommandId => throw new NotImplementedException();
        }
    }
}
