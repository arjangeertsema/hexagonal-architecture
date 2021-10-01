using System.Threading.Tasks;

namespace Reference.Domain.Abstractions
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}