using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class nullableCreditSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_CreditSource_CreditSourceID",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreditSourceID",
                schema: "TAM",
                table: "Contracts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_CreditSource_CreditSourceID",
                schema: "TAM",
                table: "Contracts",
                column: "CreditSourceID",
                principalSchema: "TAM",
                principalTable: "CreditSource",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_CreditSource_CreditSourceID",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreditSourceID",
                schema: "TAM",
                table: "Contracts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_CreditSource_CreditSourceID",
                schema: "TAM",
                table: "Contracts",
                column: "CreditSourceID",
                principalSchema: "TAM",
                principalTable: "CreditSource",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
