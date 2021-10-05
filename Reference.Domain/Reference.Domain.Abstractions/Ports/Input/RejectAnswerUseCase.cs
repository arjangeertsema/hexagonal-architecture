using System;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Ports;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class RejectAnswerUseCase : IInputPort, IUserTask
    {

        public RejectAnswerUseCase(Guid commandId, Guid questionId, long userTaskId, string rejection)
        {
            if (string.IsNullOrWhiteSpace(rejection))
            {
                throw new ArgumentException($"'{nameof(rejection)}' cannot be null or whitespace.", nameof(rejection));
            }

            CommandId = commandId;
            QuestionId = questionId;
            UserTaskId = userTaskId;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; }
        public long UserTaskId { get; set; }
        public string Rejection { get; set; }
    }
}
