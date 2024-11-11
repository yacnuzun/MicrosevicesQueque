using Microsoft.EntityFrameworkCore;

namespace AccountApi.Entities.DbConnectionContext
{
    public class AccountDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=pg-1825034b-yacn-4ec5.c.aivencloud.com:24051; Database=userdb; Username=avnadmin; Password=AVNS_tM18fCWJ4a0Hhvht1Cz");
        }

        // User Authentication
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
