using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Reference.Domain.Abstractions;
using Reference.UseCases.Attributes;

namespace Reference.UseCases.Behaviours
{
    public class TransactionalBehavior : ICommandBehaviour
    {
        public async Task Handle(ICommand command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, CommandHandlerDelegate next)
        {
            var attr = attributeCollection.GetAttribute<TransactionalAttribute>();
            if( attr == null)
            {
                await next();
                return;
            }
            
            using(var scope = new TransactionScope())
            {
                await next();
                
                scope.Complete();
            }
        }
    }
}