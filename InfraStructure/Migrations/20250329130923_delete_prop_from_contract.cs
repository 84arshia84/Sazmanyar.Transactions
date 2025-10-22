using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class delete_prop_from_contract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractDateOfNotification",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractEndDate",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractExchangeDate",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractPeriod",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractStartDate",
                schema: "TAM",
                table: "Contracts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ContractDateOfNotification",
                schema: "TAM",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractEndDate",
                schema: "TAM",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractExchangeDate",
                schema: "TAM",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractPeriod",
                schema: "TAM",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractStartDate",
                schema: "TAM",
                table: "Contracts",
                type: "datetime2",
                nullable: true);
        }
    }
}
