using System.Threading;
using System.Threading.Tasks;

namespace Common.CQRS.Abstractions.Queries
{
    public interface IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
    }
}