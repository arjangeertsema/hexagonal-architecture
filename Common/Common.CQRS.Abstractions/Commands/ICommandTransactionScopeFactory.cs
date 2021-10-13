using System.Transactions;

namespace Common.CQRS.Abstractions.Commands
{
    public interface ICommandTransactionScopeFactory<TCommand>
        where TCommand : ICommand
    {
        TransactionScope Create();
    }
}