using System.Threading.Tasks;

namespace Reference.Domain.Abstractions
{
    public interface IMediator
    {
        Task Send(ICommand command);
        Task<TResponse> Send<TResponse>(IQuery<TResponse> query);
    }
}