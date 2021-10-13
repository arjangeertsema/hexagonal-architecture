using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Events;
using Common.CQRS.Abstractions.Queries;

namespace Common.CQRS.Abstractions
{
    public interface IMediator
    {
        Task Send(ICommand command, CancellationToken cancellationToken = default);
        Task<TResponse> Ask<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
        Task Notify(IEvent @event, CancellationToken cancellationToken = default);
    }
}