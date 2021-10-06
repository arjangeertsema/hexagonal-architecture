using System.Threading;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Events;
using Synion.CQRS.Abstractions.Queries;

namespace Synion.CQRS.Abstractions
{
    public interface IMediator
    {
        Task Send(ICommand command, CancellationToken cancellationToken = default);
        Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
        Task Notify(IEvent @event, CancellationToken cancellationToken = default);
    }
}