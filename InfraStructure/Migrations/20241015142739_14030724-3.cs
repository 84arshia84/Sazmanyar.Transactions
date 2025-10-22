using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class _140307243 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorespondAndTypeOfCoopRel_CorespondentReal_CorespondentRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel");

            migrationBuilder.DropForeignKey(
                name: "FK_CorespondAndTypeOfCoopRel_TypeOfCooperation_TypeOfCooperationID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel");

            migrationBuilder.DropIndex(
                name: "IX_CorespondAndTypeOfCoopRel_CorespondentRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel");

            migrationBuilder.DropIndex(
                name: "IX_CorespondAndTypeOfCoopRel_TypeOfCooperationID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel");

            migrationBuilder.DropColumn(
                name: "CorespondentRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel");

            migrationBuilder.DropColumn(
                name: "TypeOfCooperationID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CorespondentRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TypeOfCooperationID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CorespondAndTypeOfCoopRel_CorespondentRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "CorespondentRealID");

            migrationBuilder.CreateIndex(
                name: "IX_CorespondAndTypeOfCoopRel_TypeOfCooperationID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "TypeOfCooperationID");

            migrationBuilder.AddForeignKey(
                name: "FK_CorespondAndTypeOfCoopRel_CorespondentReal_CorespondentRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "CorespondentRealID",
                principalSchema: "TAM",
                principalTable: "CorespondentReal",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CorespondAndTypeOfCoopRel_TypeOfCooperation_TypeOfCooperationID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "TypeOfCooperationID",
                principalSchema: "TAM",
                principalTable: "TypeOfCooperation",
                principalColumn: "ID");
        }
    }
}
