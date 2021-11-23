namespace Adapters.EF;

[ServiceLifetime(ServiceLifetime.Scoped)]
public class OutboxStore :
    IQueryHandler<GetDomainEvents, IEnumerable<GetDomainEvents.Response>>,
    IQueryHandler<GetLastPublishedEventId, Guid>,
    ICommandHandler<SetLastPublishedEventId>,
    ICommandHandler<PublishDomainEvent>
{
    private readonly DBContext dbContext;
    private readonly IMediator mediator;
    private readonly IDeserializer deserializer;

    public OutboxStore(DBContext dbContext, IMediator mediator, IDeserializer deserializer)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
    }

    public async Task<IEnumerable<GetDomainEvents.Response>> Handle(GetDomainEvents query, CancellationToken cancellationToken)
    {
        var offset = await CalculateOffsetId(query.EventIdOffset);

        var events = await dbContext.DomainEvents
            .OrderBy(e => e.Id)
            .Where(e => e.Id > offset)
            .Take(query.BatchSize)
            .ToListAsync();

        return events
            .Select(e => new GetDomainEvents.Response(e.EventId, Type.GetType(e.Type), e.Event));
    }

    public async Task Handle(SetLastPublishedEventId command, CancellationToken cancellationToken)
    {       
        
        var model = await dbContext.LastPublishedEvent.FirstOrDefaultAsync();
        if (model == null)
        {
            model.EventId = command.EventId;
            model.Published = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
        }
        else
        {
            await dbContext.LastPublishedEvent.AddAsync(new LastPublishedEventModel()
            {
                EventId = command.EventId,
                Published = DateTime.UtcNow
            });
        }        
    }

    public async Task<Guid> Handle(GetLastPublishedEventId query, CancellationToken cancellationToken)
    {
        return await dbContext.LastPublishedEvent
            .Select(e => e.EventId)
            .FirstOrDefaultAsync();
    }

    public async Task Handle(PublishDomainEvent command, CancellationToken cancellationToken)
    {
        var @event = await deserializer.Deserialize(command.EventType, command.Event);
        await mediator.Notify((IEvent)@event, cancellationToken);
    }

    private async Task<long> CalculateOffsetId(Guid eventIdOffset)
    {
        if(!eventIdOffset.Equals(default(Guid)))
            return 0;

        return await dbContext.DomainEvents
            .Where(e => e.EventId.Equals(eventIdOffset))
            .Select(e => e.Id)
            .FirstAsync();
    }
}
