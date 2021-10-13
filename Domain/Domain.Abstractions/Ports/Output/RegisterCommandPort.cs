using System;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Queries;

namespace Domain.Abstractions.Ports.Output
{
    public class RegisterCommandPort : ICommand
    {
        public RegisterCommandPort(ICommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            Command = command;
        }

        public Guid CommandId { get {return Command.CommandId; } }
        public ICommand Command { get; }
    }
}