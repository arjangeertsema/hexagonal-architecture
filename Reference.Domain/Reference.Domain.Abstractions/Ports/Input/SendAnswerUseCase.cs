using System;
using Synion.CQRS.Abstractions.Ports;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class SendAnswerUseCase : IInputPort
    {
        public SendAnswerUseCase(Guid commandId, Guid questionId)
        {
            CommandId = commandId;
            QuestionId = questionId;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; }
    }
}
