using Microsoft.EntityFrameworkCore;

namespace BuyerAPI.Entities.DbConnectionContext
{
    public class BuyerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=pg-1825034b-yacn-4ec5.c.aivencloud.com:24051; Database=buyerdb; Username=avnadmin; Password=AVNS_tM18fCWJ4a0Hhvht1Cz");
        }
        public DbSet<Buyer> Buyers { get; set; }

    }
}
