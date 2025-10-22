using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class initPriceList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentID",
                schema: "TAM",
                table: "CreditSource",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PriceList",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceList", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PriceListCource",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourceTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListCource", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceListCource_PriceList_PriceListID",
                        column: x => x.PriceListID,
                        principalSchema: "TAM",
                        principalTable: "PriceList",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceListClause",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClauseTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceListCourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListClause", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceListClause_PriceListCource_PriceListCourceID",
                        column: x => x.PriceListCourceID,
                        principalSchema: "TAM",
                        principalTable: "PriceListCource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceListExplanation",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceListClauseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListExplanation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceListExplanation_PriceListClause_PriceListClauseID",
                        column: x => x.PriceListClauseID,
                        principalSchema: "TAM",
                        principalTable: "PriceListClause",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceListClause_PriceListCourceID",
                schema: "TAM",
                table: "PriceListClause",
                column: "PriceListCourceID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListCource_PriceListID",
                schema: "TAM",
                table: "PriceListCource",
                column: "PriceListID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListExplanation_PriceListClauseID",
                schema: "TAM",
                table: "PriceListExplanation",
                column: "PriceListClauseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceListExplanation",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "PriceListClause",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "PriceListCource",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "PriceList",
                schema: "TAM");

            migrationBuilder.DropColumn(
                name: "ParentID",
                schema: "TAM",
                table: "CreditSource");
        }
    }
}
