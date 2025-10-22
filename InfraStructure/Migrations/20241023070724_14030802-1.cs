using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class _140308021 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContractTimeProfiles_BasisForStartingProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_CreditSourceID",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_OrganizationUnitID",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_TransActionTypeID",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTimeProfiles_BasisForStartingProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles",
                column: "BasisForStartingProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CreditSourceID",
                schema: "TAM",
                table: "Contracts",
                column: "CreditSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OrganizationUnitID",
                schema: "TAM",
                table: "Contracts",
                column: "OrganizationUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TransActionTypeID",
                schema: "TAM",
                table: "Contracts",
                column: "TransActionTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContractTimeProfiles_BasisForStartingProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_CreditSourceID",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_OrganizationUnitID",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_TransActionTypeID",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTimeProfiles_BasisForStartingProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles",
                column: "BasisForStartingProjectID",
                unique: true,
                filter: "[BasisForStartingProjectID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CreditSourceID",
                schema: "TAM",
                table: "Contracts",
                column: "CreditSourceID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OrganizationUnitID",
                schema: "TAM",
                table: "Contracts",
                column: "OrganizationUnitID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TransActionTypeID",
                schema: "TAM",
                table: "Contracts",
                column: "TransActionTypeID",
                unique: true);
        }
    }
}
