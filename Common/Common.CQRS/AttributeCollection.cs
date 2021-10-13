using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.CQRS.Abstractions;

namespace Common.CQRS
{
    public class AttributeCollection : IAttributeCollection
    {
        private readonly IEnumerable<Attribute> attributes;

        public AttributeCollection(IEnumerable<Attribute> attributes)
        {
            if (attributes is null)
            {
                throw new ArgumentNullException(nameof(attributes));
            }

            this.attributes = attributes;
        }

        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
        {
            var type = typeof(TAttribute);

            return this.attributes
                .Where(a => a.GetType().Equals(type))
                .Select(a => (TAttribute)a);
        }

        public TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute => GetAttributes<TAttribute>().SingleOrDefault();

        public TAttribute GetRequiredAttribute<TAttribute>() where TAttribute : Attribute => GetAttributes<TAttribute>().Single();

        public IEnumerator<Attribute> GetEnumerator() => attributes.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => attributes.GetEnumerator();
    }
}