using System.Threading.Tasks;
using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Queries;

namespace Synion.CQRS.Abstractions
{
    public interface IMediator
    {
        Task Send(ICommand command);
        Task<TResponse> Send<TResponse>(IQuery<TResponse> query);
    }
}