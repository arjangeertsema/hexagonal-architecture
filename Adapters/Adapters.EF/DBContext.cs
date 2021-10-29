using Adapters.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Adapters.EF
{
    public class DBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<DomainEventModel> DomainEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
            modelBuilder.Entity<DomainEventModel>()
                .HasKey(e => new { e.AggregateRootId, e.Version });
        }
    }
}