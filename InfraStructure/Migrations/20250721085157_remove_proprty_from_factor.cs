using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class remove_proprty_from_factor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factors_FactorForms_FactorFormId",
                schema: "TAM",
                table: "Factors");

            migrationBuilder.DropIndex(
                name: "IX_Factors_FactorFormId",
                schema: "TAM",
                table: "Factors");

            migrationBuilder.DropColumn(
                name: "FactorFormId",
                schema: "TAM",
                table: "Factors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FactorFormId",
                schema: "TAM",
                table: "Factors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factors_FactorFormId",
                schema: "TAM",
                table: "Factors",
                column: "FactorFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factors_FactorForms_FactorFormId",
                schema: "TAM",
                table: "Factors",
                column: "FactorFormId",
                principalSchema: "TAM",
                principalTable: "FactorForms",
                principalColumn: "ID");
        }
    }
}
