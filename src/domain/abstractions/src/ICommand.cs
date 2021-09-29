using System;

namespace Reference.Domain.Abstractions
{
    public interface ICommand
    {
        Guid CommandId { get; }
    }
}