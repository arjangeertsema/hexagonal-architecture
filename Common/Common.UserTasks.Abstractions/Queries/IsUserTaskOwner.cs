using Common.CQRS.Abstractions.Queries;

namespace Common.UserTasks.Abstractions.Queries
{
    public class IsUserTaskOwner : IQuery<bool>
    {
        private string userTaskId;

        public IsUserTaskOwner(string userTaskId)
        {
            this.userTaskId = userTaskId;
        }
    }
}