using Common.CQRS.Abstractions.Queries;

namespace Domain.Abstractions.Ports.Output
{
    public class IsUserTaskOwnerPort : IQuery<bool>
    {
        public IsUserTaskOwnerPort(long userTaskId)
        {
            UserTaskId = userTaskId;
        }
        
        public long UserTaskId { get; }
    }
}