namespace Adapters.EF.Models;

public class DomainEventModel
{
    public long Id { get; set; }
    public Guid EventId { get; set; }
    public string AggregateRootId { get; set; }    
    public long Version { get; set; }
    public DateTime Created { get; set; }
    public string Assembly { get; set; }
    public string Type { get; set; }
    public string Event { get; set; }
}
