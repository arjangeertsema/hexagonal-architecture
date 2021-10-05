using System;
using Synion.CQRS.Abstractions.Commands;

namespace Domain.Abstractions.Ports.Output.Exceptions
{
    public class DuplicateCommandIdException : Exception
    {
        public DuplicateCommandIdException(ICommand command) 
          : base($"The Command with id `{command.CommandId}` was already registered with a different command body.") { }
    }
}