using System;

namespace Reference.Domain.Abstractions.Ports.Output.Exceptions
{
    public class CommandAlreadyExistsException : Exception
    {
        public CommandAlreadyExistsException(ICommand command) 
          : base($"The Command with id `{command.CommandId}` was already registered.") { }
    }
}