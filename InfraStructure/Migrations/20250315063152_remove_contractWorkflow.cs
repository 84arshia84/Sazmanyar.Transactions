using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class remove_contractWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ContractWorkFlow_ContractWorkFlowId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "ContractWorkFlow",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractWorkFlowHistory",
                schema: "TAM");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ContractWorkFlowId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractWorkFlowId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentStageId",
                schema: "TAM",
                table: "Contracts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CurrentStatusTitle",
                schema: "TAM",
                table: "Contracts",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalApprove",
                schema: "TAM",
                table: "Contracts",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastActionTitle",
                schema: "TAM",
                table: "Contracts",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStageId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CurrentStatusTitle",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IsFinalApprove",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LastActionTitle",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.AddColumn<Guid>(
                name: "ContractWorkFlowId",
                schema: "TAM",
                table: "Contracts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContractWorkFlow",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentStageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentStatusTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    LastActionTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractWorkFlow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractWorkFlowHistory",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionerFullQualifyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActionerName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PerformedAction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractWorkFlowHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractWorkFlowHistory_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "TAM",
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractWorkFlowId",
                schema: "TAM",
                table: "Contracts",
                column: "ContractWorkFlowId",
                unique: true,
                filter: "[ContractWorkFlowId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContractWorkFlowHistory_ContractId",
                schema: "TAM",
                table: "ContractWorkFlowHistory",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ContractWorkFlow_ContractWorkFlowId",
                schema: "TAM",
                table: "Contracts",
                column: "ContractWorkFlowId",
                principalSchema: "TAM",
                principalTable: "ContractWorkFlow",
                principalColumn: "Id");
        }
    }
}
