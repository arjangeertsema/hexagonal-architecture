namespace Adapters.EF;

public class DBContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    public DbSet<AnswerQuestion> AnswerQuestions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnswerQuestion>()
            .HasKey(m => m.Id);
    }
}
