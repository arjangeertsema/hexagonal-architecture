using System.Threading.Tasks;

namespace Synion.CQRS.Abstractions.Commands
{
    public interface ICommandAuthorization<TCommand>
        where TCommand : ICommand
    {
        Task<bool> IsAuthorized(string identity, TCommand command);
    }
}