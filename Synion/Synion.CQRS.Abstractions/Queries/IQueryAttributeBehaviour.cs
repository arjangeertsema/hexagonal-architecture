using System.Threading;
using System.Threading.Tasks;

namespace Synion.CQRS.Abstractions.Queries
{
    public interface IQueryAttributeBehaviour<TQuery, TResponse, TAttribute>
        where TQuery : IQuery<TResponse>
        where TAttribute : BehaviourAttribute
    {
        Task<TResponse> Handle(TQuery query, TAttribute attribute, CancellationToken cancellationToken, QueryBehaviourDelegate<TResponse> next);
    }
}