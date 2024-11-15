using Microsoft.EntityFrameworkCore;

namespace BillApi.Entities.DbConnectionContext
{
    public class BillDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=pg-1825034b-yacn-4ec5.c.aivencloud.com:24051; Database=billdb; Username=avnadmin; Password=AVNS_tM18fCWJ4a0Hhvht1Cz");
        }
        public DbSet<Bill> Bills { get; set; }
    }
}
