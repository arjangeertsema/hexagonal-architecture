using System;
using System.Collections.Generic;
using System.Linq;
using Synion.CQRS.Abstractions;

namespace Synion.CQRS
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