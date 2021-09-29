using System;

namespace example.domain.abstractions.ports.output.exceptions
{
    public class DuplicateCommandIdException : Exception
    {
        public DuplicateCommandIdException(ICommand command) 
          : base($"The Command with id `{command.CommandId}` was already registered with a different command body.") { }
    }
}