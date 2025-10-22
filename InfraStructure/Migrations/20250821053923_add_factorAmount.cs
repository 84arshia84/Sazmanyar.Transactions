using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_factorAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FactorAmount",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedAmount = table.Column<decimal>(type: "Decimal(30,5)", nullable: false, defaultValue: 0m),
                    NettedAmount = table.Column<decimal>(type: "Decimal(30,5)", nullable: false, defaultValue: 0m),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorAmount_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "TAM",
                        principalTable: "Currency",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactorAmount_Factors_FactorId",
                        column: x => x.FactorId,
                        principalSchema: "TAM",
                        principalTable: "Factors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FactorAmount_CurrencyId",
                schema: "TAM",
                table: "FactorAmount",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorAmount_FactorId",
                schema: "TAM",
                table: "FactorAmount",
                column: "FactorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactorAmount",
                schema: "TAM");
        }
    }
}
