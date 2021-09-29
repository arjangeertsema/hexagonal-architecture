using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public interface ISendAnswerUseCase : IInputPort<ISendAnswerUseCase.Command>
    {
        public class Command : ICommand
        {
            public Command(Guid commandId)
            {
                CommandId = commandId;
            }

            public Guid CommandId { get; }
        }
    }
}
