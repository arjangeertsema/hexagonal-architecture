using System;
using System.Collections.Generic;
using example.domain.abstractions.ddd;

namespace example.domain.abstractions.ports.output
{
    public interface IStoreDomainEvents<TAggregateId> : IOutputPort<IStoreAggregateRoot<TAggregateId>.Command>
    {
        public class Command : ICommand
        {
            public Command(Guid commandId, ICollection<IDomainEvent<TAggregateId>> @events)
            {
                CommandId = commandId;
                Events = @events ?? throw new ArgumentNullException(nameof(events));
            }

            public Guid CommandId { get; }

            ICollection<IDomainEvent<TAggregateId>> Events { get; }
        }
    }
}