using System;
using System.Collections.Generic;

namespace Reference.Domain.Abstractions
{
    public interface IAttributeCollection
    {
        TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute;
        IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute;
        TAttribute GetRequiredAttribute<TAttribute>() where TAttribute : Attribute;
        IEnumerable<TAttribute> GetRequiredAttributes<TAttribute>() where TAttribute : Attribute;
    }
}