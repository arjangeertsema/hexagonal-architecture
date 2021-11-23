namespace Adapters.EF.Models
{
    public class LastPublishedEventModel
    {        
        public Guid EventId { get; set; }
        public DateTime Published { get; set; }

    }
}