using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class changeInvoiceAccessGroupPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Settings",
                schema: "TAM",
                table: "InvoiceAccessGroupPermissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Settings",
                schema: "TAM",
                table: "InvoiceAccessGroupPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
