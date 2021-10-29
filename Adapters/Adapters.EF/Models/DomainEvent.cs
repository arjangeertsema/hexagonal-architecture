using System;

namespace Adapters.EF.Models
{
    public class DomainEventModel
    {
        public Guid EventId { get; set; }
        public Guid AggregateRootId { get; set; }
        public long Version { get; set; }
        public string Assembly { get; set; }
        public string Type { get; set; }
        public string Event { get; set; }
    }
}