using Microsoft.EntityFrameworkCore;
using SupplierAPI.Constants;

namespace SupplierAPI.Entities.DbConectionContext
{
    public class SuplierDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionStringConstant.ConnectionString);
        }

        public DbSet<Supplier> Suppliers { get; set; }

        //QueueMessages table
        public DbSet<QueueMessage> QueueMessages { get; set; }
    }
}
