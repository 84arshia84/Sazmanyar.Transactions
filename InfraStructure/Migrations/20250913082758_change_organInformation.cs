using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class change_organInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceBaseInformations_AccountId",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceBaseInformations_Accounts_AccountId",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                column: "AccountId",
                principalSchema: "TAM",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceBaseInformations_Accounts_AccountId",
                schema: "TAM",
                table: "InvoiceBaseInformations");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceBaseInformations_AccountId",
                schema: "TAM",
                table: "InvoiceBaseInformations");

            migrationBuilder.DropColumn(
                name: "AccountId",
                schema: "TAM",
                table: "InvoiceBaseInformations");
        }
    }
}
