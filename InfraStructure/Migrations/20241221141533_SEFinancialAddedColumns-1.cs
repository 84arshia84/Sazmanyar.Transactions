using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class SEFinancialAddedColumns1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedRequestedPercent",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedRequestedPrice",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedRequestedVolume",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceExplanationId",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedRequestedPercent",
                schema: "TAM",
                table: "ServiceExplanationFinancials");

            migrationBuilder.DropColumn(
                name: "ApprovedRequestedPrice",
                schema: "TAM",
                table: "ServiceExplanationFinancials");

            migrationBuilder.DropColumn(
                name: "ApprovedRequestedVolume",
                schema: "TAM",
                table: "ServiceExplanationFinancials");

            migrationBuilder.DropColumn(
                name: "ServiceExplanationId",
                schema: "TAM",
                table: "ServiceExplanationFinancials");
        }
    }
}
