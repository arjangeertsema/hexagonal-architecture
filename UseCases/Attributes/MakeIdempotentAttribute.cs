using System;
using Common.CQRS.Abstractions.Attributes;

namespace UseCases.Attributes
{
    public class MakeIdempotentAttribute : BehaviourAttribute
    {
        public MakeIdempotentAttribute()
        { }
    }
}