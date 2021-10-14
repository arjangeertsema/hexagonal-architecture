using System;

namespace Common.DDD.Abstractions
{
    public class VersionedDomainEvent<TEvent> : IVersionedDomainEvent<TEvent>
        where TEvent : class, IDomainEvent
    {
        public Guid EventId => Event.EventId;
        public Guid AggregateRootId => Event.AggregateRootId;
        public int Version { get; }
        public TEvent Event { get; }
        private readonly int version;

        public VersionedDomainEvent(int version, TEvent @event)
        {
            this.version = version;
            Event = @event ?? throw new ArgumentNullException(nameof(@event));
        }

    }
}