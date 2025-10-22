using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class chaange_databaseOf_serviceExplanation_and_conrtractEstimatedMeter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RawPrice",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "Decimal(30,5)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RawPrice",
                schema: "TAM",
                table: "ContractEstimatedmeters");
        }
    }
}
