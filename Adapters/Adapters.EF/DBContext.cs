namespace Adapters.EF;

public class DBContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
