
using System.Collections.Generic;
using System.Linq;

namespace example.domain.abstractions.ddd
{
    public abstract class AggregateRoot<TId> : IAggregateRoot<TId>, IEventSourcedAggregateRoot<TId>
    {
        public const long NewAggregateVersion = -1;
        private readonly ICollection<IDomainEvent<TId>> _uncommittedEvents = new LinkedList<IDomainEvent<TId>>();
        private long _version = NewAggregateVersion;
        public TId Id { get; protected set;  }

        long IEventSourcedAggregateRoot<TId>.Version => _version;

        void IEventSourcedAggregateRoot<TId>.ApplyEvent(IDomainEvent<TId> @event, long version)
        {
            if (!_uncommittedEvents.Any(x => Equals(x.EventId, @event.EventId)))
            {
                ((dynamic)this).Apply((dynamic)@event);
                _version = version;
            }
        }

        void IEventSourcedAggregateRoot<TId>.ClearUncommittedEvents() => _uncommittedEvents.Clear();

        IEnumerable<IDomainEvent<TId>> IEventSourcedAggregateRoot<TId>.GetUncommittedEvents() => _uncommittedEvents.AsEnumerable();

        protected void RaiseEvent<TEvent>(TEvent @event)
            where TEvent: DomainEvent<TId>
        {
            IDomainEvent<TId> eventWithAggregate = @event.WithAggregate
            (
                Equals(Id, default(TId)) ? @event.AggregateId : Id,
                _version
            );

            ((IEventSourcedAggregateRoot<TId>)this).ApplyEvent(eventWithAggregate, _version + 1);
            _uncommittedEvents.Add(@event);
        }
    }
}