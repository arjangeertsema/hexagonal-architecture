
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reference.Domain.Abstractions.DDD
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly ICollection<IDomainEvent> changes;

        public AggregateRoot(Guid id)
        {
            Id = id;
            changes = new LinkedList<IDomainEvent>();
        }

        public AggregateRoot(IEnumerable<KeyValuePair<string, string>> state)
        {
            if (state is null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            changes = new LinkedList<IDomainEvent>();
        }

        public Guid Id { get; }

        IEnumerable<IDomainEvent> IAggregateRoot.GetChanges() => changes.AsEnumerable();

        void IAggregateRoot.ClearChanges() => changes.Clear();

        protected void RaiseEvent<TEvent>(TEvent @event)
            where TEvent: IDomainEvent
        {
            changes.Add(@event);
        }

        
    }
}