using System;
using Synion.CQRS.Abstractions;

namespace Reference.UseCases.Attributes
{
    public class MakeIdempotentAttribute : BehaviourAttribute
    {
        public MakeIdempotentAttribute()
        { }
    }
}