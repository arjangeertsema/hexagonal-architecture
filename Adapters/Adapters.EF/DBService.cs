namespace Adapters.EF;

[ServiceLifetime(ServiceLifetime.Scoped)]
public class DBService<TEvent, TId> :
    ICommandHandler<SaveAggregateRootEvent<TId>>,
    IQueryHandler<GetAggregateRootEvents<TId>, IEnumerable<IVersionedDomainEvent<TId>>>,
    IQueryHandler<GetUnpublishedDomainEvents, IEnumerable<IDomainEvent<TId>>>,
    ICommandHandler<SetDomainEventPublished>
    where TEvent : IVersionedDomainEvent<TId>
    where TId : IEntityId
{
    private readonly DBContext dbContext;
    private readonly ISerializer serializer;
    private readonly IDeserializer deserializer;
    public DBService(DBContext dbContext, ISerializer serializer, IDeserializer deserializer)
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
            AggregateRootId = @event.AggregateRootId,
            Version = @event.Version,
            Type = @event.GetType().FullName,
            Event = serialized
        };

        await dbContext.DomainEvents.AddAsync(model);
    }

    private async Task CheckVersions(IVersionedDomainEvent<TId> @event)
    {
        var version = await dbContext.DomainEvents
            .Where(e => e.AggregateRootId == @event.AggregateRootId)
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
            .Where(e => e.AggregateRootId == query.AggregateRootId)
            .OrderBy(e => e.Version)
            .ToListAsync();

        return await Map(events);
    }

    public async Task<IEnumerable<IDomainEvent<TId>>> Handle(GetUnpublishedDomainEvents query, CancellationToken cancellationToken)
    {
        var events = await dbContext.DomainEvents
            .Where(e => e.Published.HasValue == false)
            .OrderBy(e => e.Created)
            .Take(query.BatchSize)
            .ToListAsync();

        return await Map(events);
    }

    public async Task Handle(SetDomainEventPublished command, CancellationToken cancellationToken)
    {
        var model = await dbContext.DomainEvents.FirstOrDefaultAsync(e =>
            e.AggregateRootId == command.DomainEvent.AggregateRootId &&
            e.EventId == command.DomainEvent.EventId);

        if (model == null)
            throw new KeyNotFoundException($"DomainEvent not found for aggregate root {command.DomainEvent.AggregateRootId} with id {command.DomainEvent.EventId}.");

        if (model.Published.HasValue)
            return;

        model.Published = DateTime.UtcNow;
        await dbContext.SaveChangesAsync();
    }

    private async Task<IEnumerable<IVersionedDomainEvent<TId>>> Map(List<DomainEventModel> events)
    {
        var tasks = events.Select(Map);
        await Task.WhenAll(tasks);
        return tasks.Select(t => t.Result);
    }

    private Task<IVersionedDomainEvent> Map(DomainEventModel model)
    {
        var type = Type.GetType(model.Type);
        return deserializer.Deserialize(type, model.Event)
            .ContinueWith(t => (IVersionedDomainEvent)t.Result);
    }
}
