namespace example.domain.abstractions.ddd
{
    public interface IAggregateRoot<TId>
    {
        TId Id { get; }
    }
}