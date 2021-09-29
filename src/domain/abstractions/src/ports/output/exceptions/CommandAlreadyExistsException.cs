using System;

namespace example.domain.abstractions.ports.output.exceptions
{
    public class CommandAlreadyExistsException : Exception
    {
        public CommandAlreadyExistsException(ICommand command) 
          : base($"The Command with id `{command.CommandId}` was already registered.") { }
    }
}