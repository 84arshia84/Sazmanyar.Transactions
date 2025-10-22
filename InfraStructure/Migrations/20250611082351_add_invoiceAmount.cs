using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_invoiceAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceAmountId",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InvoiceAmounts",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedAmount = table.Column<decimal>(type: "Decimal(30,5)", nullable: false, defaultValue: 0m),
                    ApprovedAmount = table.Column<decimal>(type: "Decimal(30,5)", nullable: false, defaultValue: 0m),
                    NettingAmount = table.Column<decimal>(type: "Decimal(30,5)", nullable: false, defaultValue: 0m),
                    InvocieBaseInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceAmounts_InvoiceBaseInformations_InvocieBaseInformationId",
                        column: x => x.InvocieBaseInformationId,
                        principalSchema: "TAM",
                        principalTable: "InvoiceBaseInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAmounts_InvocieBaseInformationId",
                schema: "TAM",
                table: "InvoiceAmounts",
                column: "InvocieBaseInformationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceAmounts",
                schema: "TAM");

            migrationBuilder.DropColumn(
                name: "InvoiceAmountId",
                schema: "TAM",
                table: "InvoiceBaseInformations");
        }
    }
}
