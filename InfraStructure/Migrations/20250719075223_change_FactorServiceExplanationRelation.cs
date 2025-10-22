using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class change_FactorServiceExplanationRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactorServiceExplanations_Activitycenters_ActivityCenterID",
                schema: "TAM",
                table: "FactorServiceExplanations");

            migrationBuilder.DropIndex(
                name: "IX_FactorServiceExplanations_ActivityCenterID",
                schema: "TAM",
                table: "FactorServiceExplanations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FactorServiceExplanations_ActivityCenterID",
                schema: "TAM",
                table: "FactorServiceExplanations",
                column: "ActivityCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_FactorServiceExplanations_Activitycenters_ActivityCenterID",
                schema: "TAM",
                table: "FactorServiceExplanations",
                column: "ActivityCenterID",
                principalSchema: "TAM",
                principalTable: "Activitycenters",
                principalColumn: "ID");
        }
    }
}
