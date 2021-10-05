using System.Transactions;

namespace Reference.Domain.Abstractions
{
    public interface ICommandTransactionScopeFactory<TCommand>
        where TCommand : ICommand
    {
        TransactionScope Create();
    }
}