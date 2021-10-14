using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.DDD.Abstractions
{
    public abstract class EventSourcedAggregateRoot : IEventSourcedAggregateRoot
    {
        private readonly ICollection<IDomainEvent> changes;        
        public long Version { get; private set; }

        public EventSourcedAggregateRoot()
        {
            changes = new List<IDomainEvent>();
            Version = 0;
        }

        public Guid Id { get; private set; }

        public void ApplyEvent(IDomainEvent @event, long version)
        {
            var expectedVersion = Version - 1;

            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            if(version != expectedVersion)
            {
                throw new ArgumentOutOfRangeException($"Expected domain event version {expectedVersion} but recieved {version}.");
            }

            if(Version == 1)
            {
                Id = @event.AggregateRootId;
            }

            if(Id != @event.AggregateRootId)
            {
                throw new ArgumentOutOfRangeException($"Expected aggregate root id {Id} but recieved {@event.AggregateRootId}.");
            }

            ((dynamic)this).Apply((dynamic)@event);
            Version++;
        }

        public IEnumerable<IVersionedDomainEvent<IDomainEvent>> Commit()
        {
            var commit = this.changes
                .Select(e => CreateVersionedDomainEvent(++Version, e))
                .ToList();
            
            this.changes.Clear();
            return commit;
        }        

        protected void RaiseEvent<TEvent>(TEvent @event)
            where TEvent: IDomainEvent
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            if(Version == 1)
            {
                Id = @event.AggregateRootId;
            }

            ((dynamic)this).Apply((dynamic)@event);
            changes.Add(@event);
        }

        private static IVersionedDomainEvent<IDomainEvent> CreateVersionedDomainEvent(long version, IDomainEvent @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            var genericType = typeof(VersionedDomainEvent<>)
                .MakeGenericType(@event.GetType());

            return (IVersionedDomainEvent<IDomainEvent>) Activator.CreateInstance(genericType, new object[] { version, @event });
        }
    }}