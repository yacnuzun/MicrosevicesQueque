using Microsoft.EntityFrameworkCore;

namespace SupplierAPI.Entities.DbConectionContext
{
    public class SuplierDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=pg-1825034b-yacn-4ec5.c.aivencloud.com:24051; Database=suplierdb; Username=avnadmin; Password=AVNS_tM18fCWJ4a0Hhvht1Cz");
        }

        public DbSet<Supplier> Suppliers { get; set; }

        //QueueMessages table
        public DbSet<QueueMessage> QueueMessages { get; set; }
    }
}
