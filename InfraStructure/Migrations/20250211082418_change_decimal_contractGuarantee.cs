using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class change_decimal_contractGuarantee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GuaranteePrice",
                schema: "TAM",
                table: "ContractGuarantees",
                type: "Decimal(30,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(31,5)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GuaranteePrice",
                schema: "TAM",
                table: "ContractGuarantees",
                type: "Decimal(31,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(30,5)");
        }
    }
}
