using System;
using System.Collections.Generic;

namespace example.domain.abstractions.ports.output
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