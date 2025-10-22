using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class fixinvoiceaccessgroupgroupfkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupParentId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceAccessGroupGroups_AccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceAccessGroupGroups_AccessGroupParentId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups");

            migrationBuilder.DropColumn(
                name: "AccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups");

            migrationBuilder.DropColumn(
                name: "AccessGroupParentId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_GroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "GroupId",
                principalSchema: "TAM",
                principalTable: "InvoiceAccessGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_ParentGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "ParentGroupId",
                principalSchema: "TAM",
                principalTable: "InvoiceAccessGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_GroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_ParentGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "AccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessGroupParentId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupGroups_AccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "AccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupGroups_AccessGroupParentId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "AccessGroupParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "AccessGroupId",
                principalSchema: "TAM",
                principalTable: "InvoiceAccessGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupParentId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "AccessGroupParentId",
                principalSchema: "TAM",
                principalTable: "InvoiceAccessGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
