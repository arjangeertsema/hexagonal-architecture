using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public interface IAcceptAnswerUseCase : IInputPort<IAcceptAnswerUseCase.Command>
    {
        public class Command : ICommand
        {
            public Command(Guid commandId, long taskId, Guid questionId)
            {
                CommandId = commandId;
                TaskId = taskId;
                QuestionId = questionId;
            }
            public Guid CommandId { get; }

            public long TaskId { get; }
            public Guid QuestionId { get; }
        }
    }
}
