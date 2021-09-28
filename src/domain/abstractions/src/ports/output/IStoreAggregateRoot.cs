using System;
using example.domain.abstractions.ddd;

namespace example.domain.abstractions.ports.output
{
    public interface ISaveAggregateRoot<TAggregateId> : IOutputPort<ISaveAggregateRoot<TAggregateId>.Command>
    {
        public class Command : ICommand
        {
            public Command(Guid commandId, IEventSourcedAggregateRoot<TAggregateId> aggregateRoot)
            {
                CommandId = commandId;
                AggregateRoot = aggregateRoot ?? throw new System.ArgumentNullException(nameof(aggregateRoot));
            }

            public Guid CommandId { get; }

            IEventSourcedAggregateRoot<TAggregateId> AggregateRoot { get; }
        }
    }
}