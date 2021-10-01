using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class MakeIdempotentAttribute : Attribute
    {
        public MakeIdempotentAttribute()
        { }
    }
}