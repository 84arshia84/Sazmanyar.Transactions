using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoicePayment_Rel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceBaseInformationId",
                schema: "TAM",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceBaseInformationId",
                schema: "TAM",
                table: "Payments",
                column: "InvoiceBaseInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_InvoiceBaseInformations_InvoiceBaseInformationId",
                schema: "TAM",
                table: "Payments",
                column: "InvoiceBaseInformationId",
                principalSchema: "TAM",
                principalTable: "InvoiceBaseInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_InvoiceBaseInformations_InvoiceBaseInformationId",
                schema: "TAM",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_InvoiceBaseInformationId",
                schema: "TAM",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "InvoiceBaseInformationId",
                schema: "TAM",
                table: "Payments");
        }
    }
}
