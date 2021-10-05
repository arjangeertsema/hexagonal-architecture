using System;
using Synion.CQRS.Abstractions.Commands;

namespace Domain.Abstractions.Ports.Output.Exceptions
{
    public class CommandAlreadyExistsException : Exception
    {
        public CommandAlreadyExistsException(ICommand command) 
          : base($"The Command with id `{command.CommandId}` was already registered.") { }
    }
}