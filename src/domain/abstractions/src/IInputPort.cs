using System.Threading.Tasks;

namespace Reference.Domain.Abstractions
{
    public interface IInputPort<TCommand>  
        where TCommand : ICommand
    { 
        Task Execute(TCommand command);
    }

    public interface IInputPort<TQuery, TResponse>
         where TQuery : IQuery<TResponse>
         where TResponse : struct
    { 
        Task<TResponse> Execute(TQuery query);
    }
}
