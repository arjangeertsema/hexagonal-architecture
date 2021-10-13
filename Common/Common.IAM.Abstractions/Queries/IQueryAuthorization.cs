using System.Threading.Tasks;
using Common.CQRS.Abstractions.Queries;

namespace Common.IAM.Abstractions.Queries
{
    public interface IQueryAuthorization<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task Authorize(string identity, TQuery query, TResponse response);
    }
}