using AccountApi.Constants;
using Microsoft.EntityFrameworkCore;

namespace AccountApi.Entities.DbConnectionContext
{
    public class AccountDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionStringConstant.ConnectionString);
        }

        // User Authentication
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
