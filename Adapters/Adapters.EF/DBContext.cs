using Adapters.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Adapters.EF
{
    public class DBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<DomainEventModel> DomainEvents { get; set; }
    }
}