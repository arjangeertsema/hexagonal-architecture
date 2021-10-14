using System;
using Common.CQRS.Abstractions.Commands;

namespace Common.CQRS.Abstractions.Exceptions
{
    public class CommandIsAlreadyHandledException : Exception
    {
        public CommandIsAlreadyHandledException(ICommand command) 
          : base($"The Command with id `{command.CommandId}` was already registered.") { }
    }
}