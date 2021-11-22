namespace Adapters.EF;

[ServiceLifetime(ServiceLifetime.Scoped)]
public class EventStore<TId> :
    ICommandHandler<SaveAggregateRootEvent<TId>>,
    IQueryHandler<GetAggregateRootEvents<TId>, IEnumerable<IVersionedDomainEvent<TId>>>
    where TId : IEntityId
{
    private readonly DBContext dbContext;
    private readonly ISerializer serializer;
    private readonly IDeserializer deserializer;
    public EventStore(DBContext dbContext, ISerializer serializer, IDeserializer deserializer)
    {
        this.dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        this.serializer = serializer ?? throw new System.ArgumentNullException(nameof(serializer));
        this.deserializer = deserializer ?? throw new System.ArgumentNullException(nameof(deserializer));
    }

    public async Task Handle(SaveAggregateRootEvent<TId> command, CancellationToken cancellationToken)
    {
        var @event = (IVersionedDomainEvent<TId>)command.Event;

        await CheckVersions(@event);

        var serialized = await serializer.Serialize(@event);

        var model = new DomainEventModel()
        {
            EventId = @event.EventId,
            Created = DateTime.UtcNow,
            AggregateRootId = @event.AggregateRootId.ToString(),
            Version = @event.Version,
            Type = @event.GetType().FullName,
            Event = serialized
        };

        await dbContext.DomainEvents.AddAsync(model);
    }

    private async Task CheckVersions(IVersionedDomainEvent<TId> @event)
    {
        var version = await dbContext.DomainEvents
            .Where(e => e.AggregateRootId.Equals(@event.AggregateRootId.ToString()))
            .Select(e => e.Version)
            .DefaultIfEmpty()
            .MaxAsync();

        if ((@event.Version == 1 && version != default(long)) ||
            (version == default(long) && @event.Version != 1) ||
            (version != default(long) && version == @event.Version - 1))
        {
            throw new Exception($"Database version {version} and event version {@event.Version} are out of bounds.");
        }
    }

    public async Task<IEnumerable<IVersionedDomainEvent<TId>>> Handle(GetAggregateRootEvents<TId> query, CancellationToken cancellationToken)
    {
        var events = await dbContext.DomainEvents
            .Where(e => e.AggregateRootId.Equals(query.AggregateRootId.ToString()))
            .OrderBy(e => e.Version)
            .ToListAsync();

        return await Map(events);
    }

    private async Task<IEnumerable<IVersionedDomainEvent<TId>>> Map(List<DomainEventModel> events)
    {
        var tasks = events.Select(Map);
        await Task.WhenAll(tasks);
        return tasks.Select(t => t.Result);
    }

    private Task<IVersionedDomainEvent<TId>> Map(DomainEventModel model)
    {
        var type = Type.GetType(model.Type);
        return deserializer.Deserialize(type, model.Event)
            .ContinueWith(t => (IVersionedDomainEvent<TId>)t.Result);
    }
}
