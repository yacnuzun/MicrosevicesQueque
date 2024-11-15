﻿// <auto-generated />
using BillApi.Entities.DbConnectionContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BillApi.Migrations
{
    [DbContext(typeof(BillDbContext))]
    partial class BillDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BillApi.Entities.Bill", b =>
                {
                    b.Property<int>("BillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BillID"));

                    b.Property<string>("BuyerTaxID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("InovoiceStatus")
                        .HasColumnType("integer");

                    b.Property<decimal>("InvoiceCost")
                        .HasColumnType("numeric");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SuplierTaxID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TermDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BillID");

                    b.ToTable("Bills");
                });
#pragma warning restore 612, 618
        }
    }
}
