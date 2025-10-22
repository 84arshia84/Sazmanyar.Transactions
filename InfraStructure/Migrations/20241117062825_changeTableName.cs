using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class changeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListClause_PriceListCource_PriceListCourceID",
                schema: "TAM",
                table: "PriceListClause");

            migrationBuilder.DropTable(
                name: "PriceListCource",
                schema: "TAM");

            migrationBuilder.CreateTable(
                name: "PriceListField",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourceTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListField", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceListField_PriceList_PriceListID",
                        column: x => x.PriceListID,
                        principalSchema: "TAM",
                        principalTable: "PriceList",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceListField_PriceListID",
                schema: "TAM",
                table: "PriceListField",
                column: "PriceListID");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListClause_PriceListField_PriceListCourceID",
                schema: "TAM",
                table: "PriceListClause",
                column: "PriceListCourceID",
                principalSchema: "TAM",
                principalTable: "PriceListField",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListClause_PriceListField_PriceListCourceID",
                schema: "TAM",
                table: "PriceListClause");

            migrationBuilder.DropTable(
                name: "PriceListField",
                schema: "TAM");

            migrationBuilder.CreateTable(
                name: "PriceListCource",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourceTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_PriceListCource_PriceListID",
                schema: "TAM",
                table: "PriceListCource",
                column: "PriceListID");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListClause_PriceListCource_PriceListCourceID",
                schema: "TAM",
                table: "PriceListClause",
                column: "PriceListCourceID",
                principalSchema: "TAM",
                principalTable: "PriceListCource",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
