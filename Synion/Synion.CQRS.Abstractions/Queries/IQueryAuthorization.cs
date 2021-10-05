using System.Threading.Tasks;

namespace Synion.CQRS.Abstractions.Queries
{
    public interface IQueryAuthorization<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<bool> IsAuthorized(string identity, TQuery query, TResponse response);
    }
}