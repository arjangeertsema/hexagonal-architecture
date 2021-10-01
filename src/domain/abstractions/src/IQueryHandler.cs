using System.Threading.Tasks;

namespace Reference.Domain.Abstractions
{
    public interface IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query);
    }
}