using System;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public class RegisterCommand : IOutputPort
    {
        public RegisterCommand(ICommand command)
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