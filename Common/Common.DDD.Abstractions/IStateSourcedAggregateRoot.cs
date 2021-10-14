using System.Collections.Generic;

namespace Common.DDD.Abstractions
{
    public interface IStateSourcedAggregateRoot<TState> : IAggregateRoot
        where TState : IAggregateRootState, new()
    {
        void ApplyState(TState state);
        IEnumerable<IDomainEvent> Commit();
    }
}