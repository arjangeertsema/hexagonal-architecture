using System;

namespace example.domain.abstractions.ports.input
{
    public interface IModifyAnswerUseCase : IInputPort<IModifyAnswerUseCase.Command>
    {
        public class Command : ICommand
        {
            public Guid CommandId => throw new NotImplementedException();
        }
    }
}
