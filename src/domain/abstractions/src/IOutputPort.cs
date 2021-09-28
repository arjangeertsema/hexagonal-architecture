using System.Threading.Tasks;

namespace example.domain.abstractions
{
    public interface IOutputPort<TCommand>  
        where TCommand : ICommand
    { 
        Task Execute(TCommand command);
    }

    public interface IOutputPort<TQuery, TResponse>
         where TQuery : IQuery<TResponse>
         where TResponse : struct
    { 
         Task<TQuery> Execute(TQuery query);
    }
}
