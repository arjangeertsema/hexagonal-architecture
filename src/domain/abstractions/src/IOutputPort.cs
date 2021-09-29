using System.Threading.Tasks;

namespace Reference.Domain.Abstractions
{
    public interface IOutputPort<TCommand>  
        where TCommand : ICommand
    { 
        Task Execute(TCommand command);
    }

    public interface IOutputPort<TQuery, TResponse>
         where TQuery : IQuery<TResponse>
    { 
         Task<TResponse> Execute(TQuery query);
    }
}
