using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class addCurrentAmountOfInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentApproveInvoiceAmount",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                type: "Decimal(30,5)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentApproveInvoiceAmount",
                schema: "TAM",
                table: "InvoiceBaseInformations");
        }
    }
}
