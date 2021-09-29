using System;
using static Reference.Domain.Abstractions.Ports.Input.IRegisterQuestionUseCase;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public interface IRegisterQuestionUseCase : IInputPort<Command>
    {
        class Command : ICommand
        {
            public Command(Guid commandId, string subject, string question, string sender)
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
                Subject  = subject;
                Question = question;
                Sender = sender;
            }
            public Guid CommandId { get; }
            public string Subject { get; }
            public string Question { get; }            
            public string Sender { get; }
        }
    }
}
