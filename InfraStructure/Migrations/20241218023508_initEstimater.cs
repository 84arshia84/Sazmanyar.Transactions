using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class initEstimater : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractEstimatedmeters",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Field = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClauseId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Explenation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExplenationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoefficientTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SumOfCoefficients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractEstimatedmeters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractEstimatedmeters_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "TAM",
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractEstimatedmeters_ContractId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractEstimatedmeters",
                schema: "TAM");
        }
    }
}
