using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class SendQuestionAnsweredEventUseCase : IInputPort
    {
        public SendQuestionAnsweredEventUseCase(Guid commandId)
        {
            CommandId = commandId;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; set; }
    }
}
