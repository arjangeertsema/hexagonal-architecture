using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Queries;

namespace Synion.CQRS.Abstractions.Ports
{
    public interface IInputPort : ICommand
    { }

    public interface IInputPort<out TResponse> : IQuery<TResponse>
    { }
}
