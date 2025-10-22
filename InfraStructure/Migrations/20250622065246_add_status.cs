using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                schema: "TAM",
                table: "Contracts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
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
                    table.PrimaryKey("PK_Status", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_StatusId",
                schema: "TAM",
                table: "Contracts",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Status_StatusId",
                schema: "TAM",
                table: "Contracts",
                column: "StatusId",
                principalSchema: "TAM",
                principalTable: "Status",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Status_StatusId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "Status",
                schema: "TAM");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_StatusId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "StatusId",
                schema: "TAM",
                table: "Contracts");
        }
    }
}
