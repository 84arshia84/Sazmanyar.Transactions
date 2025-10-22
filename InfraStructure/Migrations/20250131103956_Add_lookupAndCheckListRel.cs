using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_lookupAndCheckListRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ContractCheckListValues_ContractCheckListValueId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_LookUpTableAndCheckListRel_CheckLists_CheckListId",
                table: "LookUpTableAndCheckListRel");

            migrationBuilder.DropForeignKey(
                name: "FK_LookUpTableAndCheckListRel_LookUpTables_LookUpTableId",
                table: "LookUpTableAndCheckListRel");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ContractCheckListValueId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LookUpTableAndCheckListRel",
                table: "LookUpTableAndCheckListRel");

            migrationBuilder.RenameTable(
                name: "ContractCheckListValues",
                newName: "ContractCheckListValues",
                newSchema: "TAM");

            migrationBuilder.RenameTable(
                name: "LookUpTableAndCheckListRel",
                newName: "LookUpTableAndCheckListRels",
                newSchema: "TAM");

            migrationBuilder.RenameIndex(
                name: "IX_LookUpTableAndCheckListRel_LookUpTableId",
                schema: "TAM",
                table: "LookUpTableAndCheckListRels",
                newName: "IX_LookUpTableAndCheckListRels_LookUpTableId");

            migrationBuilder.RenameIndex(
                name: "IX_LookUpTableAndCheckListRel_CheckListId",
                schema: "TAM",
                table: "LookUpTableAndCheckListRels",
                newName: "IX_LookUpTableAndCheckListRels_CheckListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LookUpTableAndCheckListRels",
                schema: "TAM",
                table: "LookUpTableAndCheckListRels",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCheckListValues_ContractId",
                schema: "TAM",
                table: "ContractCheckListValues",
                column: "ContractId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractCheckListValues_Contracts_ContractId",
                schema: "TAM",
                table: "ContractCheckListValues",
                column: "ContractId",
                principalSchema: "TAM",
                principalTable: "Contracts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LookUpTableAndCheckListRels_CheckLists_CheckListId",
                schema: "TAM",
                table: "LookUpTableAndCheckListRels",
                column: "CheckListId",
                principalSchema: "TAM",
                principalTable: "CheckLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LookUpTableAndCheckListRels_LookUpTables_LookUpTableId",
                schema: "TAM",
                table: "LookUpTableAndCheckListRels",
                column: "LookUpTableId",
                principalSchema: "TAM",
                principalTable: "LookUpTables",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractCheckListValues_Contracts_ContractId",
                schema: "TAM",
                table: "ContractCheckListValues");

            migrationBuilder.DropForeignKey(
                name: "FK_LookUpTableAndCheckListRels_CheckLists_CheckListId",
                schema: "TAM",
                table: "LookUpTableAndCheckListRels");

            migrationBuilder.DropForeignKey(
                name: "FK_LookUpTableAndCheckListRels_LookUpTables_LookUpTableId",
                schema: "TAM",
                table: "LookUpTableAndCheckListRels");

            migrationBuilder.DropIndex(
                name: "IX_ContractCheckListValues_ContractId",
                schema: "TAM",
                table: "ContractCheckListValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LookUpTableAndCheckListRels",
                schema: "TAM",
                table: "LookUpTableAndCheckListRels");

            migrationBuilder.RenameTable(
                name: "ContractCheckListValues",
                schema: "TAM",
                newName: "ContractCheckListValues");

            migrationBuilder.RenameTable(
                name: "LookUpTableAndCheckListRels",
                schema: "TAM",
                newName: "LookUpTableAndCheckListRel");

            migrationBuilder.RenameIndex(
                name: "IX_LookUpTableAndCheckListRels_LookUpTableId",
                table: "LookUpTableAndCheckListRel",
                newName: "IX_LookUpTableAndCheckListRel_LookUpTableId");

            migrationBuilder.RenameIndex(
                name: "IX_LookUpTableAndCheckListRels_CheckListId",
                table: "LookUpTableAndCheckListRel",
                newName: "IX_LookUpTableAndCheckListRel_CheckListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LookUpTableAndCheckListRel",
                table: "LookUpTableAndCheckListRel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractCheckListValueId",
                schema: "TAM",
                table: "Contracts",
                column: "ContractCheckListValueId",
                unique: true,
                filter: "[ContractCheckListValueId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ContractCheckListValues_ContractCheckListValueId",
                schema: "TAM",
                table: "Contracts",
                column: "ContractCheckListValueId",
                principalTable: "ContractCheckListValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LookUpTableAndCheckListRel_CheckLists_CheckListId",
                table: "LookUpTableAndCheckListRel",
                column: "CheckListId",
                principalSchema: "TAM",
                principalTable: "CheckLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LookUpTableAndCheckListRel_LookUpTables_LookUpTableId",
                table: "LookUpTableAndCheckListRel",
                column: "LookUpTableId",
                principalSchema: "TAM",
                principalTable: "LookUpTables",
                principalColumn: "ID");
        }
    }
}
