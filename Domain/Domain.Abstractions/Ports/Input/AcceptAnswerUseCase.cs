using System;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Commands;

namespace Domain.Abstractions.Ports.Input
{
    public class AcceptAnswerUseCase : ICommand, IUserTask
    {
        public AcceptAnswerUseCase(Guid commandId, Guid questionId, long userTaskId)
        {
            CommandId = commandId;
            QuestionId = questionId;
            UserTaskId = userTaskId;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; }
        public long UserTaskId { get; }
    }
}
