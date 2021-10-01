namespace Reference.Domain.Abstractions
{
    public interface IInputPortHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : IInputPort
    { }

    public interface IInputPortHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IInputPort<TResponse>
    { }
}