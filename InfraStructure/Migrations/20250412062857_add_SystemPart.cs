using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_SystemPart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ContractTypeSave",
                schema: "TAM",
                table: "ContractAccessGroupProperties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OrganizationUnitSave",
                schema: "TAM",
                table: "ContractAccessGroupProperties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RoleOfOrganizationSave",
                schema: "TAM",
                table: "ContractAccessGroupProperties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ContractAccessGroupSystemParts",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemParts = table.Column<int>(type: "int", nullable: false),
                    AccessGroupProperties = table.Column<int>(type: "int", nullable: false),
                    ContractAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAccessGroupSystemParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAccessGroupSystemParts_ContractAccessGroups_ContractAccessGroupId",
                        column: x => x.ContractAccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "ContractAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupSystemParts_ContractAccessGroupId",
                schema: "TAM",
                table: "ContractAccessGroupSystemParts",
                column: "ContractAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupSystemParts_Id",
                schema: "TAM",
                table: "ContractAccessGroupSystemParts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractAccessGroupSystemParts",
                schema: "TAM");

            migrationBuilder.DropColumn(
                name: "ContractTypeSave",
                schema: "TAM",
                table: "ContractAccessGroupProperties");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitSave",
                schema: "TAM",
                table: "ContractAccessGroupProperties");

            migrationBuilder.DropColumn(
                name: "RoleOfOrganizationSave",
                schema: "TAM",
                table: "ContractAccessGroupProperties");
        }
    }
}
