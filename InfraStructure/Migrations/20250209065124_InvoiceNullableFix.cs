using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceNullableFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractGuarantees_ForGuarantee_ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractGuarantees_ReleaseCondition_ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LeadingToDate",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractGuarantees_ForGuarantee_ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees",
                column: "ForGuaranteeId",
                principalSchema: "TAM",
                principalTable: "ForGuarantee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractGuarantees_ReleaseCondition_ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees",
                column: "ReleaseConditionId",
                principalSchema: "TAM",
                principalTable: "ReleaseCondition",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractGuarantees_ForGuarantee_ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractGuarantees_ReleaseCondition_ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LeadingToDate",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractGuarantees_ForGuarantee_ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees",
                column: "ForGuaranteeId",
                principalSchema: "TAM",
                principalTable: "ForGuarantee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractGuarantees_ReleaseCondition_ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees",
                column: "ReleaseConditionId",
                principalSchema: "TAM",
                principalTable: "ReleaseCondition",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
