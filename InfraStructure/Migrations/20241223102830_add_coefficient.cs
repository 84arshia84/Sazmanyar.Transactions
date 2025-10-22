using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_coefficient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractCoefficients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CoefficientValue = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DefaultRowValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCoefficients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractCoefficients_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "TAM",
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefaultCoefficients",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DefaultCoefficient = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DefaultRows = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultCoefficients", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractCoefficients_ContractId",
                table: "ContractCoefficients",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractCoefficients");

            migrationBuilder.DropTable(
                name: "DefaultCoefficients",
                schema: "TAM");
        }
    }
}
