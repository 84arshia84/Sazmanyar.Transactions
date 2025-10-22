using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class serviceExplanationChabged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceExplanation_Currency_CurrencyID",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrencyID",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceExplanation_Currency_CurrencyID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "CurrencyID",
                principalSchema: "TAM",
                principalTable: "Currency",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceExplanation_Currency_CurrencyID",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrencyID",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceExplanation_Currency_CurrencyID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "CurrencyID",
                principalSchema: "TAM",
                principalTable: "Currency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
