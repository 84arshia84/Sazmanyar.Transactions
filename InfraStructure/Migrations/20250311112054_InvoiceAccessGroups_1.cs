using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceAccessGroups_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "InvoiceAccessGroups",
                newName: "InvoiceAccessGroups",
                newSchema: "TAM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "InvoiceAccessGroups",
                schema: "TAM",
                newName: "InvoiceAccessGroups");
        }
    }
}
