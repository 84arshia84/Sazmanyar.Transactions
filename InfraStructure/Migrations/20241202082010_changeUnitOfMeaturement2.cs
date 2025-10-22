using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class changeUnitOfMeaturement2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceExplanation_UnitOfMeasurement_UnitOfMeasurementID",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropIndex(
                name: "IX_ServiceExplanation_UnitOfMeasurementID",
                schema: "TAM",
                table: "ServiceExplanation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_UnitOfMeasurementID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "UnitOfMeasurementID");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceExplanation_UnitOfMeasurement_UnitOfMeasurementID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "UnitOfMeasurementID",
                principalSchema: "TAM",
                principalTable: "UnitOfMeasurement",
                principalColumn: "ID");
        }
    }
}
