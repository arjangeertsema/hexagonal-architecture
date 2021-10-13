using System.Threading;
using System.Threading.Tasks;

namespace Common.CQRS.Abstractions.Commands
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }
}