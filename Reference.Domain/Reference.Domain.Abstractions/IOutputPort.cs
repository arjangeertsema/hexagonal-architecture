namespace Reference.Domain.Abstractions
{
    public interface IOutputPort : ICommand
    { }

    public interface IOutputPort<out TResponse> : IQuery<TResponse>
    { }
}
