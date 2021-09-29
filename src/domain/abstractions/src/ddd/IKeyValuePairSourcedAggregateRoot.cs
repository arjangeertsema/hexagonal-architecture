using System.Collections.Generic;

namespace example.domain.abstractions.ddd
{
    public interface IEventSourcedAggregateRoot
    {
        void Initialize(IEnumerable<KeyValuePair<string, string>> state);
        IEnumerable<KeyValuePair<string, string>> State();
    }
}