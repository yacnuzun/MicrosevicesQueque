using AccountApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountApi.Infrastructure.Data
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }


        // User Authentication
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<FailureLog> FailureLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
