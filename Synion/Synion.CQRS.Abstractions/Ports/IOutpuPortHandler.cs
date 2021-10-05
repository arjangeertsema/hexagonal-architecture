using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Queries;

namespace Synion.CQRS.Abstractions.Ports
{
    public interface IOutputPortHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : IOutputPort
    { }

    public interface IOutputPortHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IOutputPort<TResponse>
    { }
}