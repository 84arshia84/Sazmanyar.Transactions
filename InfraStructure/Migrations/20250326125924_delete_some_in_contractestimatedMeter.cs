using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class delete_some_in_contractestimatedMeter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityCenter",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "ActivityCenterId",
                schema: "TAM",
                table: "ContractEstimatedmeters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivityCenter",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ActivityCenterId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
