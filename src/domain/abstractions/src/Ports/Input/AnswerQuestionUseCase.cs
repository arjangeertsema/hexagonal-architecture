using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class AnswerQuestionUseCase : IInputPort
    {
        public AnswerQuestionUseCase(Guid commandId, Guid questionId, long taskId, string answer)
        {
            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
            }

            CommandId = commandId;
            QuestionId = questionId;
            TaskId = taskId;
            Answer = answer;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; }
        public long TaskId { get; set; }
        public string Answer { get; set; }
    }
}
