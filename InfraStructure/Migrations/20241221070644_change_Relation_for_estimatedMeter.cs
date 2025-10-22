using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class change_Relation_for_estimatedMeter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractEstimatedmeters_Contracts_ContractId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                newName: "ContractID");

            migrationBuilder.RenameIndex(
                name: "IX_ContractEstimatedmeters_ContractId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                newName: "IX_ContractEstimatedmeters_ContractID");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractID",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceExplanationId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ContractEstimatedmeters_ServiceExplanationId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ServiceExplanationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEstimatedmeters_Contracts_ContractID",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ContractID",
                principalSchema: "TAM",
                principalTable: "Contracts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEstimatedmeters_ServiceExplanation_ServiceExplanationId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ServiceExplanationId",
                principalSchema: "TAM",
                principalTable: "ServiceExplanation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractEstimatedmeters_Contracts_ContractID",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractEstimatedmeters_ServiceExplanation_ServiceExplanationId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropIndex(
                name: "IX_ContractEstimatedmeters_ServiceExplanationId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "ServiceExplanationId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.RenameColumn(
                name: "ContractID",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                newName: "ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractEstimatedmeters_ContractID",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                newName: "IX_ContractEstimatedmeters_ContractId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEstimatedmeters_Contracts_ContractId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ContractId",
                principalSchema: "TAM",
                principalTable: "Contracts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
