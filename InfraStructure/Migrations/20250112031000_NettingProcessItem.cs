using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class NettingProcessItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NettingProcessItems",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(7,5)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(30,5)", nullable: false),
                    IsDeduction = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceBaseInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NettingProcessItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NettingProcessItems_InvoiceBaseInformations_InvoiceBaseInformationId",
                        column: x => x.InvoiceBaseInformationId,
                        principalSchema: "TAM",
                        principalTable: "InvoiceBaseInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NettingProcessItems_InvoiceBaseInformationId",
                schema: "TAM",
                table: "NettingProcessItems",
                column: "InvoiceBaseInformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NettingProcessItems",
                schema: "TAM");
        }
    }
}
