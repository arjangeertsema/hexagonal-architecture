using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class RegisterQuestionUseCase : IInputPort
    {
        public RegisterQuestionUseCase(Guid commandId, string subject, string question, string askedBy)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException($"'{nameof(subject)}' cannot be null or whitespace.", nameof(subject));
            }

            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException($"'{nameof(question)}' cannot be null or empty.", nameof(question));
            }

            if (string.IsNullOrWhiteSpace(askedBy))
            {
                throw new ArgumentException($"'{nameof(askedBy)}' cannot be null or empty.", nameof(askedBy));
            }

            CommandId = commandId;
            Subject  = subject;
            Question = question;
            AskedBy = askedBy;
        }
        public Guid CommandId { get; }
        public string Subject { get; }
        public string Question { get; }            
        public string AskedBy { get; }
    }
}
