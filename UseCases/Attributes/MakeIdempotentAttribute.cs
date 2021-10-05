using System;
using Synion.CQRS.Abstractions;

namespace UseCases.Attributes
{
    public class MakeIdempotentAttribute : BehaviourAttribute
    {
        public MakeIdempotentAttribute()
        { }
    }
}