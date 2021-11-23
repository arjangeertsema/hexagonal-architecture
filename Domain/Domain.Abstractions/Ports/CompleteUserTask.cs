namespace Domain.Abstractions.Ports
{
    public class CompleteUserTask : Command
    {
        public CompleteUserTask(string userTaskId, object state = null) : base(Guid.NewGuid())
        {
            if (string.IsNullOrWhiteSpace(userTaskId))
            {
                throw new ArgumentException($"'{nameof(userTaskId)}' cannot be null or whitespace.", nameof(userTaskId));
            }

            UserTaskId = userTaskId;
            State = state;
        }

        public string UserTaskId { get; }
        public object State { get; }
    }
}