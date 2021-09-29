using System;

namespace Reference.Domain.Abstractions.DDD.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {

        public NotFoundException(string message) : base(message) { }
    }
}