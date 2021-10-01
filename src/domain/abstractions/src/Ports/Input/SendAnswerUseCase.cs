using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class SendAnswerUseCase : IInputPort
    {
        public SendAnswerUseCase(Guid commandId)
        {
            CommandId = commandId;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; set; }
    }
}
