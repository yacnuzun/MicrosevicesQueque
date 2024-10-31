using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class editabletables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierTaxID",
                table: "Suppliers",
                newName: "TaxID");

            migrationBuilder.RenameColumn(
                name: "BuyerTaxID",
                table: "Buyers",
                newName: "TaxID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxID",
                table: "Suppliers",
                newName: "SupplierTaxID");

            migrationBuilder.RenameColumn(
                name: "TaxID",
                table: "Buyers",
                newName: "BuyerTaxID");
        }
    }
}
