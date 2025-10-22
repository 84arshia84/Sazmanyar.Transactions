using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "InvoiceBaseCustomizations",
                newName: "InvoiceBaseCustomizations",
                newSchema: "TAM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "InvoiceBaseCustomizations",
                schema: "TAM",
                newName: "InvoiceBaseCustomizations");
        }
    }
}
