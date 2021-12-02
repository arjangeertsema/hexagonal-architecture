using Adapters.EF.Models;

namespace Adapters.EF;

public class DbContextAdapter : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContextAdapter(DbContextOptions<DbContextAdapter> options) : base(options) { }

    public DbSet<AnswerQuestion> AnswerQuestions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnswerQuestion>()
            .HasKey(m => m.Id);
    }
}
