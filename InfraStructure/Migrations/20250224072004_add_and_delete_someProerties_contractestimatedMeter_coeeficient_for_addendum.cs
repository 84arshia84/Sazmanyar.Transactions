using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_and_delete_someProerties_contractestimatedMeter_coeeficient_for_addendum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateInAddendumDate",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                schema: "TAM",
                table: "ContractCoefficients");

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedInAddedumId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateInAddendumId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedInAddedumId",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateInAddendumId",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedInAddedumId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "UpdateInAddendumId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "DeletedInAddedumId",
                schema: "TAM",
                table: "ContractCoefficients");

            migrationBuilder.DropColumn(
                name: "UpdateInAddendumId",
                schema: "TAM",
                table: "ContractCoefficients");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateInAddendumDate",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "datetime2",
                nullable: true);
        }
    }
}
