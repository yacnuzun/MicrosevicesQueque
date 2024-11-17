﻿using BillApi.Constants;
using Microsoft.EntityFrameworkCore;

namespace BillApi.Entities.DbConnectionContext
{
    public class BillDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionStringConstant.ConnectionString);
        }
        public DbSet<Bill> Bills { get; set; }
    }
}
