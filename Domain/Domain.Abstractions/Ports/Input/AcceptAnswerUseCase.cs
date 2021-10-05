using System;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Ports;

namespace Domain.Abstractions.Ports.Input
{
    public class AcceptAnswerUseCase : IInputPort, IUserTask
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
