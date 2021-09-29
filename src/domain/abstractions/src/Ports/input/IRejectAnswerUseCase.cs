using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public interface IRejectAnswerUseCase : IInputPort<IRejectAnswerUseCase.Command>
    {
        public class Command : ICommand
        {
            public Guid CommandId => throw new NotImplementedException();
        }
    }
}
