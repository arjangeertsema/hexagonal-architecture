using System.Threading.Tasks;

namespace Reference.Domain.Abstractions
{
    public interface ICommandAuthorization<TCommand>
        where TCommand : ICommand
    {
        Task<bool> IsAuthorized(string identity, TCommand command);
    }
}