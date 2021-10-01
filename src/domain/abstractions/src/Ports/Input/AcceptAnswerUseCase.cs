using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class AcceptAnswerUseCase : IInputPort
    {
        public AcceptAnswerUseCase(Guid commandId, Guid questionId, long taskId)
        {
            CommandId = commandId;
            QuestionId = questionId;
            TaskId = taskId;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; }
        public long TaskId { get; }
    }
}
