using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class ignore_From_ContractAccessGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessGroupPermissionsId",
                schema: "TAM",
                table: "ContractAccessGroups");

            migrationBuilder.DropColumn(
                name: "AccessGroupPropertiesId",
                schema: "TAM",
                table: "ContractAccessGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccessGroupPermissionsId",
                schema: "TAM",
                table: "ContractAccessGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessGroupPropertiesId",
                schema: "TAM",
                table: "ContractAccessGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
