using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Entities;
using Shared.Helpers.Security.JWT;
using Shared.Repositories.Implemantations;
using Shared.Repositories.Interfaces;
using System.Drawing;

namespace Shared.Entities.DbConnectionContext
{
    public class SupplyChainDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=pg-1825034b-yacn-4ec5.c.aivencloud.com:24051; Database=test; Username=avnadmin; Password=AVNS_tM18fCWJ4a0Hhvht1Cz");
        }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        // User Authentication
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        //QueueMessages table
        public DbSet<QueueMessage> QueueMessages { get; set; }

    }
}
