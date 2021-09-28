using System.Threading.Tasks;

namespace example.domain.abstractions
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
