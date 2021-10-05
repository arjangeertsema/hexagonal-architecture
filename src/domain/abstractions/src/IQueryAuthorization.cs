using System.Threading.Tasks;

namespace Reference.Domain.Abstractions
{
    public interface IQueryAuthorization<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<bool> IsAuthorized(string identity, TQuery query, TResponse response);
    }
}