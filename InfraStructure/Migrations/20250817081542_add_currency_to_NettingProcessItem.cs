using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_currency_to_NettingProcessItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                schema: "TAM",
                table: "NettingProcessItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NettingProcessItems_CurrencyId",
                schema: "TAM",
                table: "NettingProcessItems",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_NettingProcessItems_Currency_CurrencyId",
                schema: "TAM",
                table: "NettingProcessItems",
                column: "CurrencyId",
                principalSchema: "TAM",
                principalTable: "Currency",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NettingProcessItems_Currency_CurrencyId",
                schema: "TAM",
                table: "NettingProcessItems");

            migrationBuilder.DropIndex(
                name: "IX_NettingProcessItems_CurrencyId",
                schema: "TAM",
                table: "NettingProcessItems");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                schema: "TAM",
                table: "NettingProcessItems");
        }
    }
}
