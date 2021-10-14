using System;
using Common.CQRS.Abstractions.Commands;

namespace Common.CQRS.Abstractions.Exceptions
{
    public class DuplicateCommandIdException : Exception
    {
        public DuplicateCommandIdException(ICommand command) 
          : base($"The Command with id `{command.CommandId}` was already registered with a different command body.") { }
    }
}