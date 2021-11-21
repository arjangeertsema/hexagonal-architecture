namespace Domain.Abstractions.Ports
{
    public class CompleteUserTask : ICommand
    {
        public CompleteUserTask(Guid commandId, string userTaskId, object state = null)
        {
            if (string.IsNullOrWhiteSpace(userTaskId))
            {
                throw new ArgumentException($"'{nameof(userTaskId)}' cannot be null or whitespace.", nameof(userTaskId));
            }

            CommandId = commandId;
            UserTaskId = userTaskId;
            State = state;
        }

        public Guid CommandId { get; }
        public string UserTaskId { get; }
        public object State { get; }
    }
}