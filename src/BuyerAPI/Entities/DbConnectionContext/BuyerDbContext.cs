using BuyerAPI.Constants;
using Microsoft.EntityFrameworkCore;

namespace BuyerAPI.Entities.DbConnectionContext
{
    public class BuyerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionStringConstant.ConnectionString);
        }
        public DbSet<Buyer> Buyers { get; set; }

    }
}
