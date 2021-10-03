using System;
using System.Collections.Generic;

namespace Reference.Domain.UseCases.Attributes
{
    public class HasPermissionAttribute : Attribute
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