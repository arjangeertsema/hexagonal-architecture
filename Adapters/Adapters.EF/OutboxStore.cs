namespace Adapters.EF;

[ServiceLifetime(ServiceLifetime.Scoped)]
public class OutboxStore :
    IQueryHandler<GetUnpublishedDomainEvents, IEnumerable<GetUnpublishedDomainEvents.Response>>,
    ICommandHandler<SetDomainEventPublished>
{
    private readonly DBContext dbContext;
    private readonly ISerializer serializer;
    private readonly IDeserializer deserializer;
    public OutboxStore(DBContext dbContext, ISerializer serializer, IDeserializer deserializer)
    {
        this.dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        this.serializer = serializer ?? throw new System.ArgumentNullException(nameof(serializer));
        this.deserializer = deserializer ?? throw new System.ArgumentNullException(nameof(deserializer));
    }

    public async Task<IEnumerable<GetUnpublishedDomainEvents.Response>> Handle(GetUnpublishedDomainEvents query, CancellationToken cancellationToken)
    {
        var events = await dbContext.DomainEvents
            .Where(e => e.Published.HasValue.Equals(false))
            .OrderBy(e => e.Created)
            .Take(query.BatchSize)
            .ToListAsync();

        return events
            .Select(e => new GetUnpublishedDomainEvents.Response(e.AggregateRootId, e.EventId, Type.GetType(e.Type), e.Event));
    }

    public async Task Handle(SetDomainEventPublished command, CancellationToken cancellationToken)
    {
        var model = await dbContext.DomainEvents.FirstOrDefaultAsync(e =>
            e.AggregateRootId == command.AggregateRootId &&
            e.EventId == command.EventId);

        if (model == null)
            throw new KeyNotFoundException($"DomainEvent not found for aggregate root {command.AggregateRootId} with id {command.EventId}.");

        if (model.Published.HasValue)
            return;

        model.Published = DateTime.UtcNow;
        await dbContext.SaveChangesAsync();
    }
}
