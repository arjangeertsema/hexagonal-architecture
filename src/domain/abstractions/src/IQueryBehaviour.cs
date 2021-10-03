using System.Threading;
using System.Threading.Tasks;

namespace Reference.Domain.Abstractions
{
    public delegate Task<TResponse> QueryHandlerDelegate<TResponse>();
    
    public interface IQueryBehaviour
    {
        Task<object> Handle(IQuery<object> query, IAttributeCollection attributeCollection, CancellationToken cancellationToken, QueryHandlerDelegate<object> next);
    }

    public interface IQueryBehaviour<in TQuery, TResponse>
        where TQuery : notnull, IQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query, IAttributeCollection attributeCollection, CancellationToken cancellationToken, QueryHandlerDelegate<TResponse> next);
    }
}