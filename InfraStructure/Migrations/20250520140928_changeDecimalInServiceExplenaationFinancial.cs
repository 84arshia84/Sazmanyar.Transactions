using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class changeDecimalInServiceExplenaationFinancial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RequestedPercent",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "decimal(8,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ApprovedPercent",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "decimal(8,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,5)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RequestedPercent",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "decimal(7,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ApprovedPercent",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "decimal(7,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)");
        }
    }
}
