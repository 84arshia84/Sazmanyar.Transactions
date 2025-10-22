using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class change_invoiceAmount_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceAmounts_InvocieBaseInformationId",
                schema: "TAM",
                table: "InvoiceAmounts");

            migrationBuilder.DropColumn(
                name: "InvoiceAmountId",
                schema: "TAM",
                table: "InvoiceBaseInformations");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                schema: "TAM",
                table: "InvoiceAmounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanationFinancials_CurrencyId",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAmounts_CurrencyId",
                schema: "TAM",
                table: "InvoiceAmounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAmounts_InvocieBaseInformationId",
                schema: "TAM",
                table: "InvoiceAmounts",
                column: "InvocieBaseInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceAmounts_Currency_CurrencyId",
                schema: "TAM",
                table: "InvoiceAmounts",
                column: "CurrencyId",
                principalSchema: "TAM",
                principalTable: "Currency",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceExplanationFinancials_Currency_CurrencyId",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                column: "CurrencyId",
                principalSchema: "TAM",
                principalTable: "Currency",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceAmounts_Currency_CurrencyId",
                schema: "TAM",
                table: "InvoiceAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceExplanationFinancials_Currency_CurrencyId",
                schema: "TAM",
                table: "ServiceExplanationFinancials");

            migrationBuilder.DropIndex(
                name: "IX_ServiceExplanationFinancials_CurrencyId",
                schema: "TAM",
                table: "ServiceExplanationFinancials");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceAmounts_CurrencyId",
                schema: "TAM",
                table: "InvoiceAmounts");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceAmounts_InvocieBaseInformationId",
                schema: "TAM",
                table: "InvoiceAmounts");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                schema: "TAM",
                table: "ServiceExplanationFinancials");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                schema: "TAM",
                table: "InvoiceAmounts");

            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceAmountId",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAmounts_InvocieBaseInformationId",
                schema: "TAM",
                table: "InvoiceAmounts",
                column: "InvocieBaseInformationId",
                unique: true);
        }
    }
}
