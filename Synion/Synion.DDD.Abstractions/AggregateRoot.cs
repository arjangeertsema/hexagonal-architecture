
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synion.DDD.Abstractions
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly ICollection<IDomainEvent> changes;

        public AggregateRoot(Guid id)
        {
            Id = id;            
            changes = new LinkedList<IDomainEvent>();
        }

        public AggregateRoot(Guid id, IEnumerable<KeyValuePair<string, string>> state)
        {
            if (state is null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            changes = new LinkedList<IDomainEvent>();

            throw new NotImplementedException();
        }


        public Guid Id { get; }

        public IEnumerable<IDomainEvent> Commit() 
        {
            var commit = new List<IDomainEvent>(this.changes);
            this.changes.Clear();
            return commit;
        }

        protected void RaiseEvent<TEvent>(TEvent @event)
            where TEvent: IDomainEvent
        {
            changes.Add(@event);
        }
    }
}