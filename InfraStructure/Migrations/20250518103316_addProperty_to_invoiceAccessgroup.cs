using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class addProperty_to_invoiceAccessgroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ContractTypeSave",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InvoiceTypeSave",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OrganizationUnitSave",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RoleOfOrganizationSave",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractTypeSave",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties");

            migrationBuilder.DropColumn(
                name: "InvoiceTypeSave",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitSave",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties");

            migrationBuilder.DropColumn(
                name: "RoleOfOrganizationSave",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties");
        }
    }
}
