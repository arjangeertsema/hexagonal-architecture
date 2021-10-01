using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class RejectAnswerUseCase : IInputPort
    {

        public RejectAnswerUseCase(Guid commandId, Guid questionId, long taskId, string rejection)
        {
            if (string.IsNullOrWhiteSpace(rejection))
            {
                throw new ArgumentException($"'{nameof(rejection)}' cannot be null or whitespace.", nameof(rejection));
            }

            CommandId = commandId;
            QuestionId = questionId;
            TaskId = taskId;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; }
        public long TaskId { get; set; }
        public string Rejection { get; set; }
    }
}
