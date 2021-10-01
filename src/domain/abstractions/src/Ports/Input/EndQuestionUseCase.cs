using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class EndQuestionUseCase : IInputPort
    {
        public EndQuestionUseCase(Guid commandId, Guid questionId)
        {
            CommandId = commandId;
            QuestionId = questionId;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; set; }
    }
}