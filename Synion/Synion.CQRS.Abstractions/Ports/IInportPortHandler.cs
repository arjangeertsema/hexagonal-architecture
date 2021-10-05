using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Queries;

namespace Synion.CQRS.Abstractions.Ports
{
    public interface IInputPortHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : IInputPort
    { }

    public interface IInputPortHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IInputPort<TResponse>
    { }
}