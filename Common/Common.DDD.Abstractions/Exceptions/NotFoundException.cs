using System;

namespace Common.DDD.Abstractions.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {

        public NotFoundException(string message) : base(message) { }
    }
}