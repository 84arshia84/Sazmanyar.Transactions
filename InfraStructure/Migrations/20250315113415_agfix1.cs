using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class agfix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessGroupProperties_InvoiceAccessGroups_InvoiceAccessGroupId",
                schema: "TAM",
                table: "AccessGroupProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceAccessGroupPermissions",
                schema: "TAM",
                table: "InvoiceAccessGroupPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessGroupProperties",
                schema: "TAM",
                table: "AccessGroupProperties");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "TAM",
                table: "InvoiceAccessGroupPermissions");

            migrationBuilder.RenameTable(
                name: "AccessGroupProperties",
                schema: "TAM",
                newName: "InvoiceAccessGroupProperties",
                newSchema: "TAM");

            migrationBuilder.RenameIndex(
                name: "IX_AccessGroupProperties_InvoiceAccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties",
                newName: "IX_InvoiceAccessGroupProperties_InvoiceAccessGroupId");

            migrationBuilder.AddColumn<Guid>(
                name: "asdId",
                schema: "TAM",
                table: "InvoiceAccessGroupPermissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceAccessGroupPermissions",
                schema: "TAM",
                table: "InvoiceAccessGroupPermissions",
                column: "asdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceAccessGroupProperties",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceAccessGroupProperties_InvoiceAccessGroups_InvoiceAccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties",
                column: "InvoiceAccessGroupId",
                principalSchema: "TAM",
                principalTable: "InvoiceAccessGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceAccessGroupProperties_InvoiceAccessGroups_InvoiceAccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceAccessGroupPermissions",
                schema: "TAM",
                table: "InvoiceAccessGroupPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceAccessGroupProperties",
                schema: "TAM",
                table: "InvoiceAccessGroupProperties");

            migrationBuilder.DropColumn(
                name: "asdId",
                schema: "TAM",
                table: "InvoiceAccessGroupPermissions");

            migrationBuilder.RenameTable(
                name: "InvoiceAccessGroupProperties",
                schema: "TAM",
                newName: "AccessGroupProperties",
                newSchema: "TAM");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceAccessGroupProperties_InvoiceAccessGroupId",
                schema: "TAM",
                table: "AccessGroupProperties",
                newName: "IX_AccessGroupProperties_InvoiceAccessGroupId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "TAM",
                table: "InvoiceAccessGroupPermissions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceAccessGroupPermissions",
                schema: "TAM",
                table: "InvoiceAccessGroupPermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessGroupProperties",
                schema: "TAM",
                table: "AccessGroupProperties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessGroupProperties_InvoiceAccessGroups_InvoiceAccessGroupId",
                schema: "TAM",
                table: "AccessGroupProperties",
                column: "InvoiceAccessGroupId",
                principalSchema: "TAM",
                principalTable: "InvoiceAccessGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
