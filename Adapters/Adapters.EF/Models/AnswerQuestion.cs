namespace Adapters.EF.Models
{
    public class AnswerQuestion
    {
        public string Id { get; set; }
        public DateTime Asked { get; set; }
        public string AskedBy { get; set; }
        public string Subject { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime? Answered { get; set; }
        public string AnsweredBy { get; set; }
        public string Rejection { get; set; }
        public DateTime? Rejected { get; set; }
        public string RejectedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? Accepted { get; set; }
        public string AcceptedBy { get; set; }
        public DateTime? Sent { get; set; }
    }
}