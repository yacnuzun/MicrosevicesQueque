﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Shared.Entities.DbConnectionContext;

#nullable disable

namespace Shared.Migrations
{
    [DbContext(typeof(SupplyChainDbContext))]
    [Migration("20241031103418_messageQuequeupdate")]
    partial class messageQuequeupdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Shared.Entities.Bill", b =>
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

                    b.Property<DateTime>("TermDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("BillID");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Shared.Entities.Buyer", b =>
                {
                    b.Property<string>("BuyerID")
                        .HasColumnType("text");

                    b.Property<string>("BuyerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TaxID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BuyerID");

                    b.ToTable("Buyers");
                });

            modelBuilder.Entity("Shared.Entities.OperationClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OperationClaims");
                });

            modelBuilder.Entity("Shared.Entities.QueueMessage", b =>
                {
                    b.Property<int>("QueueMessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("QueueMessageID"));

                    b.Property<string>("BuyerTaxID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("InvoiceCost")
                        .HasColumnType("numeric");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("QueueGUID")
                        .HasColumnType("uuid");

                    b.Property<string>("SuplierTaxID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TermDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("QueueMessageID");

                    b.ToTable("QueueMessages");
                });

            modelBuilder.Entity("Shared.Entities.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SupplierID"));

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TaxID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SupplierID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Shared.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserTaxID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Shared.Entities.UserOperationClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("OperationClaimId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserOperationClaims");
                });
#pragma warning restore 612, 618
        }
    }
}
