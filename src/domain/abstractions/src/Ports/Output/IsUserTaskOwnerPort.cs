
namespace Reference.Domain.Abstractions.Ports.Output
{
    public class IsUserTaskOwnerPort : IOutputPort<bool>
    {
        public IsUserTaskOwnerPort(string identity, long userTaskId)
        {
            if (string.IsNullOrWhiteSpace(identity))
            {
                throw new System.ArgumentException($"'{nameof(identity)}' cannot be null or whitespace.", nameof(identity));
            }
            Identity = identity;
            UserTaskId = userTaskId;
        }

        public string Identity { get; }
        public long UserTaskId { get; }
    }
}