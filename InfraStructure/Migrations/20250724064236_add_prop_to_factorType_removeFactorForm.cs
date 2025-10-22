using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_prop_to_factorType_removeFactorForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactorForms",
                schema: "TAM");

            migrationBuilder.AddColumn<Guid>(
                name: "OfficeOnlineDocumentId",
                schema: "TAM",
                table: "FactorTypes",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficeOnlineDocumentId",
                schema: "TAM",
                table: "FactorTypes");

            migrationBuilder.CreateTable(
                name: "FactorForms",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorForms", x => x.ID);
                });
        }
    }
}
