using System;

namespace Common.CQRS.Abstractions.Commands
{
    public interface ICommand
    {
        Guid CommandId { get; }
    }
}