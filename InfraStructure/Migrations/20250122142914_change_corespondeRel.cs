using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class change_corespondeRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorespondAndTypeOfCoopRel_CorespondentReal_CoresponedRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel");

            migrationBuilder.DropIndex(
                name: "IX_CorespondAndTypeOfCoopRel_CoresponedRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel");

            migrationBuilder.DropColumn(
                name: "CoresponedRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel");

            migrationBuilder.CreateTable(
                name: "CorespondRealAntTypeOfCoopRel",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorespondentRealID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TypeOfCoopreationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TypeOfCooperationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorespondRealAntTypeOfCoopRel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CorespondRealAntTypeOfCoopRel_CorespondentReal_CorespondentRealID",
                        column: x => x.CorespondentRealID,
                        principalSchema: "TAM",
                        principalTable: "CorespondentReal",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CorespondRealAntTypeOfCoopRel_TypeOfCooperation_TypeOfCooperationID",
                        column: x => x.TypeOfCooperationID,
                        principalSchema: "TAM",
                        principalTable: "TypeOfCooperation",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorespondRealAntTypeOfCoopRel_CorespondentRealID",
                schema: "TAM",
                table: "CorespondRealAntTypeOfCoopRel",
                column: "CorespondentRealID");

            migrationBuilder.CreateIndex(
                name: "IX_CorespondRealAntTypeOfCoopRel_TypeOfCooperationID",
                schema: "TAM",
                table: "CorespondRealAntTypeOfCoopRel",
                column: "TypeOfCooperationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorespondRealAntTypeOfCoopRel",
                schema: "TAM");

            migrationBuilder.AddColumn<Guid>(
                name: "CoresponedRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CorespondAndTypeOfCoopRel_CoresponedRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "CoresponedRealID");

            migrationBuilder.AddForeignKey(
                name: "FK_CorespondAndTypeOfCoopRel_CorespondentReal_CoresponedRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "CoresponedRealID",
                principalSchema: "TAM",
                principalTable: "CorespondentReal",
                principalColumn: "ID");
        }
    }
}
