using System;

namespace Reference.Domain.Abstractions.Ports.Output.Exceptions
{
    public class DuplicateCommandIdException : Exception
    {
        public DuplicateCommandIdException(ICommand command) 
          : base($"The Command with id `{command.CommandId}` was already registered with a different command body.") { }
    }
}