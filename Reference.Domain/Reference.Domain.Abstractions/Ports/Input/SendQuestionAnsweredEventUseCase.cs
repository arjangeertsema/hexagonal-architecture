using System;
using Synion.CQRS.Abstractions.Ports;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class SendQuestionAnsweredEventUseCase : IInputPort
    {
        public SendQuestionAnsweredEventUseCase(Guid commandId, Guid questionId)
        {
            CommandId = commandId;
            QuestionId = questionId;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; }
    }
}
