namespace Common.DDD.Abstractions
{
    public interface IVersionedDomainEvent<TEvent> : IDomainEvent
        where TEvent : class, IDomainEvent
    {
         int Version { get; }
         TEvent Event { get; }
    }
}