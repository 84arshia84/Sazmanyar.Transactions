using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class changeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfCoopreationID",
                schema: "TAM",
                table: "CorespondRealAntTypeOfCoopRel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TypeOfCoopreationID",
                schema: "TAM",
                table: "CorespondRealAntTypeOfCoopRel",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
