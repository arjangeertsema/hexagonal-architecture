namespace Reference.Domain.Abstractions
{
    public interface IOutputPortHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : IOutputPort
    { }

    public interface IOutputPortHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IOutputPort<TResponse>
    { }
}