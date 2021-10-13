using Common.CQRS.Abstractions.Queries;

namespace Common.IAM.Abstractions.Queries
{
    public class HasPermission : IQuery<bool>
    {
        public HasPermission(params string[] permissions)
        {
            Permissions = permissions ?? throw new System.ArgumentNullException(nameof(permissions));
        }

        public string[] Permissions { get; }
    }
}