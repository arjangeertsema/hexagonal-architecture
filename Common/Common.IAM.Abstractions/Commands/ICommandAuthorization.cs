using System.Threading.Tasks;
using Common.CQRS.Abstractions.Commands;

namespace Common.IAM.Abstractions.Commands
{
    public interface ICommandAuthorization<TCommand>
        where TCommand : ICommand
    {
        Task Authorize(string userId, TCommand command);
    }
}