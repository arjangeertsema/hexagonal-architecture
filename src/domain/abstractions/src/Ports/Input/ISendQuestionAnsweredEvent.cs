using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public interface ISendQuestionAnsweredEventUseCase : IInputPort<ISendQuestionAnsweredEventUseCase.Command>
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
