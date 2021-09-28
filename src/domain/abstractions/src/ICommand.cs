using System;

namespace example.domain.abstractions
{
    public interface ICommand
    {
        Guid CommandId { get; }
    }
}