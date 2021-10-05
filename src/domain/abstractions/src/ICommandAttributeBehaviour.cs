using System.Threading;
using System.Threading.Tasks;

namespace Reference.Domain.Abstractions
{
    public interface ICommandAttributeBehaviour<TCommand, in TAttribute>
        where TCommand : ICommand
        where TAttribute : BehaviourAttribute
    {
        Task Handle(TCommand command, TAttribute attribute, CancellationToken cancellationToken, CommandBehaviourDelegate next);
    }
}