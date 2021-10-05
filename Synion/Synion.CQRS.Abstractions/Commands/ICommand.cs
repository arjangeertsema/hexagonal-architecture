using System;

namespace Synion.CQRS.Abstractions.Commands
{
    public interface ICommand
    {
        Guid CommandId { get; }
    }
}