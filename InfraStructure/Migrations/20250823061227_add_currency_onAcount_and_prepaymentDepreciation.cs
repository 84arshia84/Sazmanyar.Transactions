using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_currency_onAcount_and_prepaymentDepreciation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                schema: "TAM",
                table: "PrePaymentDepreciations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                schema: "TAM",
                table: "OnAccountDepreciation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrePaymentDepreciations_CurrencyId",
                schema: "TAM",
                table: "PrePaymentDepreciations",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_OnAccountDepreciation_CurrencyId",
                schema: "TAM",
                table: "OnAccountDepreciation",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnAccountDepreciation_Currency_CurrencyId",
                schema: "TAM",
                table: "OnAccountDepreciation",
                column: "CurrencyId",
                principalSchema: "TAM",
                principalTable: "Currency",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PrePaymentDepreciations_Currency_CurrencyId",
                schema: "TAM",
                table: "PrePaymentDepreciations",
                column: "CurrencyId",
                principalSchema: "TAM",
                principalTable: "Currency",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnAccountDepreciation_Currency_CurrencyId",
                schema: "TAM",
                table: "OnAccountDepreciation");

            migrationBuilder.DropForeignKey(
                name: "FK_PrePaymentDepreciations_Currency_CurrencyId",
                schema: "TAM",
                table: "PrePaymentDepreciations");

            migrationBuilder.DropIndex(
                name: "IX_PrePaymentDepreciations_CurrencyId",
                schema: "TAM",
                table: "PrePaymentDepreciations");

            migrationBuilder.DropIndex(
                name: "IX_OnAccountDepreciation_CurrencyId",
                schema: "TAM",
                table: "OnAccountDepreciation");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                schema: "TAM",
                table: "PrePaymentDepreciations");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                schema: "TAM",
                table: "OnAccountDepreciation");
        }
    }
}
