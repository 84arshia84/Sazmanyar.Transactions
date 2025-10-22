using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_addendumChangeRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddendumExplenation",
                schema: "TAM",
                table: "ServiceExplanation",
                newName: "IsAddendum");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateInAddendumDate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UpdatedInAddendum",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddendumId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContractAddendumId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAddendum",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateInAddendumDate",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UpdatedInAddendum",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAddendum",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UpdatedInAddendum",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractEstimatedmeters_ContractAddendumId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ContractAddendumId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEstimatedmeters_ContractAddendums_ContractAddendumId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ContractAddendumId",
                principalSchema: "TAM",
                principalTable: "ContractAddendums",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractEstimatedmeters_ContractAddendums_ContractAddendumId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropIndex(
                name: "IX_ContractEstimatedmeters_ContractAddendumId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropColumn(
                name: "UpdateInAddendumDate",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropColumn(
                name: "UpdatedInAddendum",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropColumn(
                name: "AddendumId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "ContractAddendumId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "IsAddendum",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "UpdateInAddendumDate",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "UpdatedInAddendum",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                schema: "TAM",
                table: "ContractCoefficients");

            migrationBuilder.DropColumn(
                name: "IsAddendum",
                schema: "TAM",
                table: "ContractCoefficients");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                schema: "TAM",
                table: "ContractCoefficients");

            migrationBuilder.DropColumn(
                name: "UpdatedInAddendum",
                schema: "TAM",
                table: "ContractCoefficients");

            migrationBuilder.RenameColumn(
                name: "IsAddendum",
                schema: "TAM",
                table: "ServiceExplanation",
                newName: "AddendumExplenation");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
