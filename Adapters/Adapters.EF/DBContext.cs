namespace Adapters.EF;

public class DBContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    public DbSet<DomainEventModel> DomainEvents { get; set; }
    public DbSet<LastPublishedEventModel> LastPublishedEvent { get; set; }
    public DbSet<CommandModel> Commands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DomainEventModel>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<DomainEventModel>()
            .Property(m => m.Id).UseIdentityColumn();

        modelBuilder.Entity<DomainEventModel>()
            .HasIndex(m => m.EventId)
            .IsUnique();

        modelBuilder.Entity<DomainEventModel>()
            .HasIndex(m => new { m.AggregateRootId, m.Version })
            .IsUnique();

        modelBuilder.Entity<LastPublishedEventModel>()
            .HasKey(m => m.EventId);

        modelBuilder.Entity<CommandModel>()
            .HasKey(m => m.CommandId);
    }
}
