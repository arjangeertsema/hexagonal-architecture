using System.Threading;
using System.Threading.Tasks;

namespace Synion.CQRS.Abstractions.Commands
{
    public delegate Task CommandBehaviourDelegate();
    
    public interface ICommandBehaviour<in TCommand>
        where TCommand : notnull, ICommand
    {
        Task Handle(TCommand command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, CommandBehaviourDelegate next);
    }
}