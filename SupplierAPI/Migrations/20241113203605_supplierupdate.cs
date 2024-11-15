using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SupplierAPI.Migrations
{
    /// <inheritdoc />
    public partial class supplierupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BuyerTaxID = table.Column<string>(type: "text", nullable: false),
                    InovoiceStatus = table.Column<int>(type: "integer", nullable: false),
                    InvoiceCost = table.Column<decimal>(type: "numeric", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: false),
                    SuplierTaxID = table.Column<string>(type: "text", nullable: false),
                    TermDate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillID);
                });
        }
    }
}
