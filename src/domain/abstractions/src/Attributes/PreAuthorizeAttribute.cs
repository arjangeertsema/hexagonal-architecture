using System;
using System.Collections.Generic;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class PreAuthorizeAttribute : Attribute
    {
        public PreAuthorizeAttribute(params string[] permissions)
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