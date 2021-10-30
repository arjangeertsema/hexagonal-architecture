using System;
using Common.CQRS.Abstractions;

namespace Domain.Abstractions.Ports
{
    public class SendMessagePort : ICommand
    {
        public Guid CommandId { get; }
        public Recipent From { get; }
        public Recipent To { get; }
        public string Subject { get; }
        public string Message { get; }
        public SendMessagePort(Guid commandId, Recipent from, Recipent to, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException($"'{nameof(subject)}' cannot be null or whitespace.", nameof(subject));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace.", nameof(message));
            }

            this.Message = message;
            this.Subject = subject;
            this.To = to;
            this.From = from;
            CommandId = commandId;
        }

        public class Recipent
        {

        }
    }
}