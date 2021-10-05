using System;
using System.Collections.Generic;

namespace Synion.CQRS.Abstractions
{
    public interface IAttributeCollection : IEnumerable<Attribute>
    {
        TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute;
        IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute;
        TAttribute GetRequiredAttribute<TAttribute>() where TAttribute : Attribute;        
    }
}