using System;
using Common.CQRS.Abstractions.Commands;

namespace Common.CQRS.Abstractions.Commands
{
    public class RegisterCommand : ICommand
    {
        public RegisterCommand(ICommand command) => Command = command ?? throw new ArgumentNullException(nameof(command));

        public Guid CommandId { get => Command.CommandId; }
        public ICommand Command { get; }
    }
}