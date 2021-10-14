using System;
using Common.CQRS.Abstractions.Commands;

namespace Domain.Abstractions.UseCases
{
    public class SendQuestionAnsweredEventUseCase : ICommand
    {
        public SendQuestionAnsweredEventUseCase(Guid commandId, Guid questionId)
        {
            CommandId = commandId;
            QuestionId = questionId;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; }
    }
}
