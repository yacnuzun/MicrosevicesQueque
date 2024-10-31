using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class messageQueque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QueueMessages",
                columns: table => new
                {
                    QueueMessageID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QueueGUID = table.Column<Guid>(type: "uuid", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: false),
                    InvoiceCost = table.Column<decimal>(type: "numeric", nullable: false),
                    TermDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BuyerTaxID = table.Column<string>(type: "text", nullable: false),
                    SuplierTaxID = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueMessages", x => x.QueueMessageID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueueMessages");
        }
    }
}
