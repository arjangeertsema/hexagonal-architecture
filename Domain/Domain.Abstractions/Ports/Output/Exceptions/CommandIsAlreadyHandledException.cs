using System;
using Common.CQRS.Abstractions.Commands;

namespace Domain.Abstractions.Ports.Output.Exceptions
{
    public class CommandIsAlreadyHandledException : Exception
    {
        public CommandIsAlreadyHandledException(ICommand command) 
          : base($"The Command with id `{command.CommandId}` was already registered.") { }
    }
}