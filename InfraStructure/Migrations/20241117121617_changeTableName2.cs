using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class changeTableName2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListClause_PriceListField_PriceListCourceID",
                schema: "TAM",
                table: "PriceListClause");

            migrationBuilder.RenameColumn(
                name: "CourceTitle",
                schema: "TAM",
                table: "PriceListField",
                newName: "FieldTitle");

            migrationBuilder.RenameColumn(
                name: "PriceListCourceID",
                schema: "TAM",
                table: "PriceListClause",
                newName: "PriceListFieldID");

            migrationBuilder.RenameIndex(
                name: "IX_PriceListClause_PriceListCourceID",
                schema: "TAM",
                table: "PriceListClause",
                newName: "IX_PriceListClause_PriceListFieldID");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListClause_PriceListField_PriceListFieldID",
                schema: "TAM",
                table: "PriceListClause",
                column: "PriceListFieldID",
                principalSchema: "TAM",
                principalTable: "PriceListField",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListClause_PriceListField_PriceListFieldID",
                schema: "TAM",
                table: "PriceListClause");

            migrationBuilder.RenameColumn(
                name: "FieldTitle",
                schema: "TAM",
                table: "PriceListField",
                newName: "CourceTitle");

            migrationBuilder.RenameColumn(
                name: "PriceListFieldID",
                schema: "TAM",
                table: "PriceListClause",
                newName: "PriceListCourceID");

            migrationBuilder.RenameIndex(
                name: "IX_PriceListClause_PriceListFieldID",
                schema: "TAM",
                table: "PriceListClause",
                newName: "IX_PriceListClause_PriceListCourceID");

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
    }
}
