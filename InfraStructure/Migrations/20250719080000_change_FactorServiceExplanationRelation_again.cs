using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class change_FactorServiceExplanationRelation_again : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactorServiceExplanations_UnitOfMeasurement_FactorId",
                schema: "TAM",
                table: "FactorServiceExplanations");

            migrationBuilder.CreateIndex(
                name: "IX_FactorServiceExplanations_UnitOfMeasurementID",
                schema: "TAM",
                table: "FactorServiceExplanations",
                column: "UnitOfMeasurementID");

            migrationBuilder.AddForeignKey(
                name: "FK_FactorServiceExplanations_UnitOfMeasurement_UnitOfMeasurementID",
                schema: "TAM",
                table: "FactorServiceExplanations",
                column: "UnitOfMeasurementID",
                principalSchema: "TAM",
                principalTable: "UnitOfMeasurement",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactorServiceExplanations_UnitOfMeasurement_UnitOfMeasurementID",
                schema: "TAM",
                table: "FactorServiceExplanations");

            migrationBuilder.DropIndex(
                name: "IX_FactorServiceExplanations_UnitOfMeasurementID",
                schema: "TAM",
                table: "FactorServiceExplanations");

            migrationBuilder.AddForeignKey(
                name: "FK_FactorServiceExplanations_UnitOfMeasurement_FactorId",
                schema: "TAM",
                table: "FactorServiceExplanations",
                column: "FactorId",
                principalSchema: "TAM",
                principalTable: "UnitOfMeasurement",
                principalColumn: "ID");
        }
    }
}
