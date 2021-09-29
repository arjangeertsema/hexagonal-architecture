using System;
using System.Collections.Generic;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public interface IHasPermissonPort : IOutputPort<IHasPermissonPort.Query, bool>
    {
        public class Query : IQuery<bool>
        {
            public Query(params string[] permissions)
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
}