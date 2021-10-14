using System;
using Common.CQRS.Abstractions.Commands;

namespace Domain.Abstractions.UseCases
{
    public class EndQuestionUseCase : ICommand
    {
        public EndQuestionUseCase(Guid commandId, Guid questionId)
        {
            CommandId = commandId;
            QuestionId = questionId;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; set; }
    }
}