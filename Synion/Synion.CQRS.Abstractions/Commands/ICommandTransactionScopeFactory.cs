using System.Transactions;

namespace Synion.CQRS.Abstractions.Commands
{
    public interface ICommandTransactionScopeFactory<TCommand>
        where TCommand : ICommand
    {
        TransactionScope Create();
    }
}