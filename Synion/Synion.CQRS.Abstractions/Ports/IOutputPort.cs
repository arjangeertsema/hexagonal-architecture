using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Queries;

namespace Synion.CQRS.Abstractions.Ports
{
    public interface IOutputPort : ICommand
    { }

    public interface IOutputPort<out TResponse> : IQuery<TResponse>
    { }
}
