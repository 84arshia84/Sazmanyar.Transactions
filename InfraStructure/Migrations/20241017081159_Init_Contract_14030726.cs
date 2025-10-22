using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class Init_Contract_14030726 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractTimeProfiles_BasisForStartingTheProject_BasisForStartingTheProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles");

            migrationBuilder.DropIndex(
                name: "IX_ContractTimeProfiles_BasisForStartingTheProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles");

            migrationBuilder.DropColumn(
                name: "BasisForStartingTheProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTimeProfiles_BasisForStartingProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles",
                column: "BasisForStartingProjectID",
                unique: true,
                filter: "[BasisForStartingProjectID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTimeProfiles_BasisForStartingTheProject_BasisForStartingProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles",
                column: "BasisForStartingProjectID",
                principalSchema: "TAM",
                principalTable: "BasisForStartingTheProject",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractTimeProfiles_BasisForStartingTheProject_BasisForStartingProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles");

            migrationBuilder.DropIndex(
                name: "IX_ContractTimeProfiles_BasisForStartingProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles");

            migrationBuilder.AddColumn<Guid>(
                name: "BasisForStartingTheProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractTimeProfiles_BasisForStartingTheProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles",
                column: "BasisForStartingTheProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTimeProfiles_BasisForStartingTheProject_BasisForStartingTheProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles",
                column: "BasisForStartingTheProjectID",
                principalSchema: "TAM",
                principalTable: "BasisForStartingTheProject",
                principalColumn: "ID");
        }
    }
}
