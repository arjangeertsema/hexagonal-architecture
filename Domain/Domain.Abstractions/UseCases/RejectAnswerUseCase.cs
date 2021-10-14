using System;
using Common.CQRS.Abstractions.Commands;
using Common.UserTasks.Abstractions;

namespace Domain.Abstractions.UseCases
{
    public class RejectAnswerUseCase : ICommand, IUserTask
    {

        public RejectAnswerUseCase(Guid commandId, Guid questionId, string userTaskId, string rejection)
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
        public string UserTaskId { get; set; }
        public string Rejection { get; set; }
    }
}
