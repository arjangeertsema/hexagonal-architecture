using System.Threading.Tasks;

namespace Reference.Domain.Abstractions
{
    public interface IHasCommandAuthorization<TCommand>
        where TCommand : ICommand
    {
        Task<bool> IsAuthorized(string identity, TCommand command);
    }
}