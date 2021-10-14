using System;
using Common.CQRS.Abstractions.Commands;
using Common.UserTasks.Abstractions;

namespace Domain.Abstractions.UseCases
{
    public class AcceptAnswerUseCase : ICommand, IUserTask
    {
        public AcceptAnswerUseCase(Guid commandId, Guid questionId, string userTaskId)
        {
            CommandId = commandId;
            QuestionId = questionId;
            UserTaskId = userTaskId;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; }
        public string UserTaskId { get; }
    }
}
