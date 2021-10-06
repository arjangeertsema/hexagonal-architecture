
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Synion.DDD.Abstractions
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly ICollection<IDomainEvent> changes;

        protected AggregateRoot(Guid id)
        {
            Id = id;            
            changes = new LinkedList<IDomainEvent>();
        }

        protected AggregateRoot(Guid id, IEnumerable<KeyValuePair<string, string>> state)
            : this(id)
        {
            if (state is null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            try
            {
                ParseState(state);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to parse state for aggregate root with id {id}.", ex);
            }
        }

        public Guid Id { get; }

        public IEnumerable<IDomainEvent> Commit() 
        {
            var commit = new List<IDomainEvent>(this.changes);
            this.changes.Clear();
            return commit;
        }

        protected void RaiseEvent<TEvent>(TEvent @event)
            where TEvent: IDomainEvent
        {
            changes.Add(@event);
        }

        private void ParseState(IEnumerable<KeyValuePair<string, string>> state)
        {
            var type = this.GetType();
            foreach(var kvp in state)
            {
                var property = type.GetProperty(kvp.Key);
                if(property == null)
                    throw new KeyNotFoundException($"Propert {kvp.Key} not found in {type.FullName}.");

                var value = Convert(property.PropertyType, kvp.Value);
                property.SetValue(this, value);
            }
        }

        private static object Convert(Type valueType, string value)
        {
            return typeof(AggregateRoot)
                .GetMethod(nameof(Convert), 1, new Type[] { typeof(string) })
                .MakeGenericMethod(valueType)
                .Invoke(null, new object[] { value });
        }

        private static T Convert<T>(string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(value));
        }
    }
}