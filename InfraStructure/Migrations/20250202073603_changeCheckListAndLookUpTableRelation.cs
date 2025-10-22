using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class changeCheckListAndLookUpTableRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LookUpTableAndCheckListRels",
                schema: "TAM");

            migrationBuilder.AddColumn<Guid>(
                name: "LookUpTableId",
                schema: "TAM",
                table: "CheckLists",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckLists_LookUpTableId",
                schema: "TAM",
                table: "CheckLists",
                column: "LookUpTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckLists_LookUpTables_LookUpTableId",
                schema: "TAM",
                table: "CheckLists",
                column: "LookUpTableId",
                principalSchema: "TAM",
                principalTable: "LookUpTables",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckLists_LookUpTables_LookUpTableId",
                schema: "TAM",
                table: "CheckLists");

            migrationBuilder.DropIndex(
                name: "IX_CheckLists_LookUpTableId",
                schema: "TAM",
                table: "CheckLists");

            migrationBuilder.DropColumn(
                name: "LookUpTableId",
                schema: "TAM",
                table: "CheckLists");

            migrationBuilder.CreateTable(
                name: "LookUpTableAndCheckListRels",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LookUpTableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpTableAndCheckListRels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LookUpTableAndCheckListRels_CheckLists_CheckListId",
                        column: x => x.CheckListId,
                        principalSchema: "TAM",
                        principalTable: "CheckLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LookUpTableAndCheckListRels_LookUpTables_LookUpTableId",
                        column: x => x.LookUpTableId,
                        principalSchema: "TAM",
                        principalTable: "LookUpTables",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LookUpTableAndCheckListRels_CheckListId",
                schema: "TAM",
                table: "LookUpTableAndCheckListRels",
                column: "CheckListId");

            migrationBuilder.CreateIndex(
                name: "IX_LookUpTableAndCheckListRels_LookUpTableId",
                schema: "TAM",
                table: "LookUpTableAndCheckListRels",
                column: "LookUpTableId");
        }
    }
}
