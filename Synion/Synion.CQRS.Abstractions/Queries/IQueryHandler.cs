using System.Threading.Tasks;

namespace Synion.CQRS.Abstractions.Queries
{
    public interface IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query);
    }
}