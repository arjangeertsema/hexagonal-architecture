namespace Reference.Domain.Abstractions
{
    public interface IInputPort : ICommand
    { }

    public interface IInputPort<out TResponse> : IQuery<TResponse>
    { }
}
