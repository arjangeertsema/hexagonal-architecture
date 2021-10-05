using System.Threading.Tasks;

namespace Synion.CQRS.Abstractions.Commands
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}