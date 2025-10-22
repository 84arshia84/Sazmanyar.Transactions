using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class fixorganizationagfkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceAccessGroupOrganizationUnits_Organizationalunit_OrganizationalUnitID",
                schema: "TAM",
                table: "InvoiceAccessGroupOrganizationUnits");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceAccessGroupOrganizationUnits_OrganizationalUnitID",
                schema: "TAM",
                table: "InvoiceAccessGroupOrganizationUnits");

            migrationBuilder.DropColumn(
                name: "OrganizationalUnitID",
                schema: "TAM",
                table: "InvoiceAccessGroupOrganizationUnits");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceAccessGroupOrganizationUnits_Organizationalunit_OrganizationUnitId",
                schema: "TAM",
                table: "InvoiceAccessGroupOrganizationUnits",
                column: "OrganizationUnitId",
                principalSchema: "TAM",
                principalTable: "Organizationalunit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceAccessGroupOrganizationUnits_Organizationalunit_OrganizationUnitId",
                schema: "TAM",
                table: "InvoiceAccessGroupOrganizationUnits");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationalUnitID",
                schema: "TAM",
                table: "InvoiceAccessGroupOrganizationUnits",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupOrganizationUnits_OrganizationalUnitID",
                schema: "TAM",
                table: "InvoiceAccessGroupOrganizationUnits",
                column: "OrganizationalUnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceAccessGroupOrganizationUnits_Organizationalunit_OrganizationalUnitID",
                schema: "TAM",
                table: "InvoiceAccessGroupOrganizationUnits",
                column: "OrganizationalUnitID",
                principalSchema: "TAM",
                principalTable: "Organizationalunit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
