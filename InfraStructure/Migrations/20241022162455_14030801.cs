using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class _14030801 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExplanationAmount",
                schema: "TAM",
                table: "ServiceExplanation",
                newName: "UnitAmount");

            migrationBuilder.AlterColumn<string>(
                name: "ProposalName",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProposalID",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExplanationStartingDate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExplanationEndingDate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "AccelerationRate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "ProgramVolume",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TotalAmount",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PercentageOfChanges",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "GoodJob_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "ContractValue_Added_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "ContractTax_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "ContractInsurance_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Amount_of_timeExtension",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgramVolume",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.RenameColumn(
                name: "UnitAmount",
                schema: "TAM",
                table: "ServiceExplanation",
                newName: "ExplanationAmount");

            migrationBuilder.AlterColumn<string>(
                name: "ProposalName",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProposalID",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExplanationStartingDate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExplanationEndingDate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccelerationRate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PercentageOfChanges",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "GoodJob_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "ContractValue_Added_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "ContractTax_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "ContractInsurance_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "Amount_of_timeExtension",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
