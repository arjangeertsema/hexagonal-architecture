using System.Collections.Generic;

namespace Reference.Domain.Abstractions.DDD
{
    public interface IEventSourcedAggregateRoot
    {
        void Initialize(IEnumerable<KeyValuePair<string, string>> state);
        IEnumerable<KeyValuePair<string, string>> State();
    }
}