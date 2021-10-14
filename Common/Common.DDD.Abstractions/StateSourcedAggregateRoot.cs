using System;
using System.Collections.Generic;

namespace Common.DDD.Abstractions
{
    public abstract class StateSourcedAggregateRoot<TState> : IStateSourcedAggregateRoot<TState>
        where TState : IAggregateRootState, new()
    {
        private readonly ICollection<IDomainEvent> changes;       
        public StateSourcedAggregateRoot()
        {
            changes = new List<IDomainEvent>();
        }

        public Guid Id { get; protected set; }
        protected TState State { get; private set; }

        public void ApplyState(TState state)
        {
            if(state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            State = state;
        }

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