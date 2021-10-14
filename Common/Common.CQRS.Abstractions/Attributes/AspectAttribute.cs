using System;

namespace Common.CQRS.Abstractions.Attributes
{
    [System.AttributeUsage(AttributeTargets.Method)]
    public abstract class AspectAttribute : Attribute
    { }
}