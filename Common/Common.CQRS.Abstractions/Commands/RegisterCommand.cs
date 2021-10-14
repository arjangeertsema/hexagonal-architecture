using System;
using Common.CQRS.Abstractions.Commands;

namespace Domain.Abstractions.Ports.Output
{
    public class RegisterCommand : ICommand
    {
        public RegisterCommand(ICommand command) => Command = command ?? throw new ArgumentNullException(nameof(command));

        public Guid CommandId { get => Command.CommandId; }
        public ICommand Command { get; }
    }
}