using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Ports.Output;
using Domain.Abstractions.Ports.Output.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS;
using Synion.CQRS.Abstractions.Ports;

namespace Adapters.Storage
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class InMemoryCommandRegistry : IOutputPortHandler<RegisterCommandPort>
    {
        private Dictionary<Guid, string> store;

        public InMemoryCommandRegistry()
        {
            this.store = new Dictionary<Guid, string>();
        }

        public Task Handle(RegisterCommandPort command, CancellationToken cancellationToken)
        {
            var hash = Hash(command);
            if(!this.store.ContainsKey(command.CommandId))
            {
                this.store.Add(command.CommandId, hash);
                return Task.CompletedTask;
            }

            if(this.store[command.CommandId] == hash)
                throw new CommandIsAlreadyHandledException(command);
            else
                throw new DuplicateCommandIdException(command);        
        }

        private string Hash(RegisterCommandPort command)
        {
            return command.GetHashCode().ToString();
        }
    }
}