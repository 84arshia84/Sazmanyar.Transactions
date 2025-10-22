using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_ContractType_CheckList_lookUpTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Section",
                schema: "TAM",
                table: "Attach");

            migrationBuilder.AddColumn<Guid>(
                name: "ContractCheckListValueId",
                schema: "TAM",
                table: "Contracts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContractCheckListValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckListValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCheckListValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypes",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LookUpTables",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpTables", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CheckLists",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContractTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckLists_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalSchema: "TAM",
                        principalTable: "ContractTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LookUpTableInsides",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LookUpTableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpTableInsides", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LookUpTableInsides_LookUpTables_LookUpTableId",
                        column: x => x.LookUpTableId,
                        principalSchema: "TAM",
                        principalTable: "LookUpTables",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LookUpTableAndCheckListRel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LookUpTableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CheckListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpTableAndCheckListRel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LookUpTableAndCheckListRel_CheckLists_CheckListId",
                        column: x => x.CheckListId,
                        principalSchema: "TAM",
                        principalTable: "CheckLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LookUpTableAndCheckListRel_LookUpTables_LookUpTableId",
                        column: x => x.LookUpTableId,
                        principalSchema: "TAM",
                        principalTable: "LookUpTables",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractCheckListValueId",
                schema: "TAM",
                table: "Contracts",
                column: "ContractCheckListValueId",
                unique: true,
                filter: "[ContractCheckListValueId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractTypeID",
                schema: "TAM",
                table: "Contracts",
                column: "ContractTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CheckLists_ContractTypeId",
                schema: "TAM",
                table: "CheckLists",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LookUpTableAndCheckListRel_CheckListId",
                table: "LookUpTableAndCheckListRel",
                column: "CheckListId");

            migrationBuilder.CreateIndex(
                name: "IX_LookUpTableAndCheckListRel_LookUpTableId",
                table: "LookUpTableAndCheckListRel",
                column: "LookUpTableId");

            migrationBuilder.CreateIndex(
                name: "IX_LookUpTableInsides_LookUpTableId",
                schema: "TAM",
                table: "LookUpTableInsides",
                column: "LookUpTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ContractCheckListValues_ContractCheckListValueId",
                schema: "TAM",
                table: "Contracts",
                column: "ContractCheckListValueId",
                principalTable: "ContractCheckListValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ContractTypes_ContractTypeID",
                schema: "TAM",
                table: "Contracts",
                column: "ContractTypeID",
                principalSchema: "TAM",
                principalTable: "ContractTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ContractCheckListValues_ContractCheckListValueId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ContractTypes_ContractTypeID",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "ContractCheckListValues");

            migrationBuilder.DropTable(
                name: "LookUpTableAndCheckListRel");

            migrationBuilder.DropTable(
                name: "LookUpTableInsides",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "CheckLists",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "LookUpTables",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractTypes",
                schema: "TAM");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ContractCheckListValueId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ContractTypeID",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractCheckListValueId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "Section",
                schema: "TAM",
                table: "Attach",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
