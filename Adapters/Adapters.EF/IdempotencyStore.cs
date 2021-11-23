namespace Adapters.EF;

[ServiceLifetime(ServiceLifetime.Scoped)]
public class IdempotencyStore : ICommandHandler<RegisterCommand>
{
    private readonly DBContext dbContext;
    public IdempotencyStore(DBContext dbContext) => this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var hashExpected = command.GetHashCode();
        var hashFound = dbContext.Commands.Where(c => c.CommandId.Equals(command.CommandId)).Select(c => c.Hash);

        if(hashFound.Equals(default(int)))
        {
            await dbContext.Commands.AddAsync(new CommandModel()
            {
                CommandId = command.CommandId,
                Hash = hashExpected
            });
        }   
        else if(hashFound.Equals(hashExpected))
        {
            throw new CommandIsAlreadyHandledException(command);
        }
        else
        {
            throw new DuplicateCommandIdException(command);
        }
    }
}
