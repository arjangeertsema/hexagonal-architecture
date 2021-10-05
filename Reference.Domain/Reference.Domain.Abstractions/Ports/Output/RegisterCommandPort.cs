using System;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public class RegisterCommandPort : IOutputPort
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