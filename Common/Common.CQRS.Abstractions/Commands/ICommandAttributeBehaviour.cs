using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions.Attributes;

namespace Common.CQRS.Abstractions.Commands
{
    public interface ICommandAttributeBehaviour<TCommand, in TAttribute>
        where TCommand : ICommand
        where TAttribute : BehaviourAttribute
    {
        Task Handle(TCommand command, TAttribute attribute, CancellationToken cancellationToken, CommandBehaviourDelegate next);
    }
}