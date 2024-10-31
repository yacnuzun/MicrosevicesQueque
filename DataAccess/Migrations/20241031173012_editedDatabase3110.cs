using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class editedDatabase3110 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "QueueMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "QueueMessages");
        }
    }
}
