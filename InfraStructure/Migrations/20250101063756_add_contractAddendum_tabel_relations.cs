using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_contractAddendum_tabel_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContractAddendumId",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "AddendumId",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddendumTypes",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddendumChangeType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddendumTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContractAddendums",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddendumDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertAddendumDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertAddendumBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteAddenDumDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAddendumBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    AddendumChangeType = table.Column<int>(type: "int", nullable: false),
                    RateOfContractPriceChanged = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddendumTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAddendums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAddendums_AddendumTypes_AddendumTypeId",
                        column: x => x.AddendumTypeId,
                        principalSchema: "TAM",
                        principalTable: "AddendumTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ContractAddendums_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "TAM",
                        principalTable: "Contracts",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_ContractAddendumId",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "ContractAddendumId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCoefficients_AddendumId",
                schema: "TAM",
                table: "ContractCoefficients",
                column: "AddendumId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAddendums_AddendumTypeId",
                schema: "TAM",
                table: "ContractAddendums",
                column: "AddendumTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAddendums_ContractId",
                schema: "TAM",
                table: "ContractAddendums",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractCoefficients_ContractAddendums_AddendumId",
                schema: "TAM",
                table: "ContractCoefficients",
                column: "AddendumId",
                principalSchema: "TAM",
                principalTable: "ContractAddendums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceExplanation_ContractAddendums_ContractAddendumId",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "ContractAddendumId",
                principalSchema: "TAM",
                principalTable: "ContractAddendums",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractCoefficients_ContractAddendums_AddendumId",
                schema: "TAM",
                table: "ContractCoefficients");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceExplanation_ContractAddendums_ContractAddendumId",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropTable(
                name: "ContractAddendums",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "AddendumTypes",
                schema: "TAM");

            migrationBuilder.DropIndex(
                name: "IX_ServiceExplanation_ContractAddendumId",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropIndex(
                name: "IX_ContractCoefficients_AddendumId",
                schema: "TAM",
                table: "ContractCoefficients");

            migrationBuilder.DropColumn(
                name: "ContractAddendumId",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropColumn(
                name: "AddendumId",
                schema: "TAM",
                table: "ContractCoefficients");
        }
    }
}
