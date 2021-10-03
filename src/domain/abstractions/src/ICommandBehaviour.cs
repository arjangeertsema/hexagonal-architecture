using System.Threading;
using System.Threading.Tasks;

namespace Reference.Domain.Abstractions
{
    public delegate Task CommandHandlerDelegate();
    
    public interface ICommandBehaviour
    {
        Task Handle(ICommand command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, CommandHandlerDelegate next);    
    }

    public interface ICommandBehaviour<in TCommand>
        where TCommand : notnull, ICommand
    {
        Task Handle(TCommand command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, CommandHandlerDelegate next);
    }
}