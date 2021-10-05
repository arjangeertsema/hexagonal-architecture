using System;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Attributes;
using Synion.CQRS.Abstractions.Commands;

namespace UseCases.Behaviours
{
    public class TransactionalCommandBehavior<TCommand> : ICommandAttributeBehaviour<TCommand, TransactionalAttribute>
        where TCommand : ICommand
    {
        private readonly ICommandTransactionScopeFactory<TCommand> transactionScopeFactory;

        public TransactionalCommandBehavior(ICommandTransactionScopeFactory<TCommand> transactionScopeFactory) => this.transactionScopeFactory = transactionScopeFactory ?? throw new ArgumentNullException(nameof(transactionScopeFactory));

        public async Task Handle(TCommand command, TransactionalAttribute attribute, CancellationToken cancellationToken, CommandBehaviourDelegate next)
        {
            using(var scope = this.transactionScopeFactory.Create())
            {
                await next();
                
                scope.Complete();
            }
        }
    }
}