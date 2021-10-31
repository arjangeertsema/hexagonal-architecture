namespace Domain.Abstractions.ValueTypes
{
    public struct Message
    {
        public Recipient From { get; }
        public Recipient To { get; }
        public string Subject { get; }
        public string Body { get; }
    }
}