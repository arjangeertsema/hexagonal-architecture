using System;
using System.Collections.Generic;
using Common.CQRS.Abstractions.Attributes;

namespace UseCases.Attributes
{
    public class HasPermissionAttribute : BehaviourAttribute
    {
        public HasPermissionAttribute(params string[] permissions)
        {
            if (permissions is null || permissions.Length == 0)
            {
                throw new ArgumentNullException(nameof(permissions));
            }
            Permissions = permissions;
        }
        public IEnumerable<string> Permissions { get; }
    }
}