using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_currencyToFactorNetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                schema: "TAM",
                table: "FactorNettingProcessItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FactorNettingProcessItems_CurrencyId",
                schema: "TAM",
                table: "FactorNettingProcessItems",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_FactorNettingProcessItems_Currency_CurrencyId",
                schema: "TAM",
                table: "FactorNettingProcessItems",
                column: "CurrencyId",
                principalSchema: "TAM",
                principalTable: "Currency",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactorNettingProcessItems_Currency_CurrencyId",
                schema: "TAM",
                table: "FactorNettingProcessItems");

            migrationBuilder.DropIndex(
                name: "IX_FactorNettingProcessItems_CurrencyId",
                schema: "TAM",
                table: "FactorNettingProcessItems");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                schema: "TAM",
                table: "FactorNettingProcessItems");
        }
    }
}
