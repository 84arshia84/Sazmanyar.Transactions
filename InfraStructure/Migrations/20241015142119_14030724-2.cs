using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class _140307242 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorespondAndTypeOfCoopRel_CorespondentLegal_CorespondentLegalID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel");

            migrationBuilder.DropIndex(
                name: "IX_CorespondAndTypeOfCoopRel_CorespondentLegalID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel");

            migrationBuilder.DropColumn(
                name: "CorespondentLegalID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CorespondentLegalID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CorespondAndTypeOfCoopRel_CorespondentLegalID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "CorespondentLegalID");

            migrationBuilder.AddForeignKey(
                name: "FK_CorespondAndTypeOfCoopRel_CorespondentLegal_CorespondentLegalID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "CorespondentLegalID",
                principalSchema: "TAM",
                principalTable: "CorespondentLegal",
                principalColumn: "ID");
        }
    }
}
