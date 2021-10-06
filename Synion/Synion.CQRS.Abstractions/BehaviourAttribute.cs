using System;

namespace Synion.CQRS.Abstractions
{
    [System.AttributeUsage(AttributeTargets.Method)]
    public abstract class BehaviourAttribute : Attribute
    { }
}