using System;
using System.Collections.Generic;
using System.Linq;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Attributes;

namespace Common.CQRS
{
    public abstract class AttributeBehaviour
    {
        private readonly Type behaviourAttributeType;

        public AttributeBehaviour()
        {
            this.behaviourAttributeType = typeof(BehaviourAttribute);            
        }

        protected IEnumerable<BehaviourAttribute> GetBehaviourAttributes(IEnumerable<Attribute> attributeCollection) 
        {
            return attributeCollection
                .Where(a => a.GetType().IsAssignableFrom(this.behaviourAttributeType))
                .Select(a => (BehaviourAttribute) a);
        }
    }
}