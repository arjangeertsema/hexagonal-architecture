using System;
using static example.domain.abstractions.ports.input.IRegisterQuestionUseCase;

namespace example.domain.abstractions.ports.input
{
    public interface IRegisterQuestionUseCase : IInputPort<Command>
    {
        class Command : ICommand
        {
            public Command(Guid commandId, Guid questionId, string subject, string question, string sender)
            {
                if (string.IsNullOrWhiteSpace(subject))
                {
                    throw new ArgumentException($"'{nameof(subject)}' cannot be null or whitespace.", nameof(subject));
                }

                if (string.IsNullOrWhiteSpace(question))
                {
                    throw new ArgumentException($"'{nameof(question)}' cannot be null or empty.", nameof(question));
                }

                if (string.IsNullOrWhiteSpace(sender))
                {
                    throw new ArgumentException($"'{nameof(sender)}' cannot be null or empty.", nameof(sender));
                }

                CommandId = commandId;                
                QuestionId = questionId;
                Subject  = subject;
                Question = question;
                Sender = sender;
            }
            public Guid CommandId { get; }
            public Guid QuestionId { get; }
            public string Subject { get; }
            public string Question { get; }            
            public string Sender { get; }
        }
    }
}
