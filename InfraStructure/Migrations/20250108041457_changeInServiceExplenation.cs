using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class changeInServiceExplenation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateInAddendumDate",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedInAddedumId",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateInAddendumId",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedInAddedumId",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropColumn(
                name: "UpdateInAddendumId",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateInAddendumDate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "datetime2",
                nullable: true);
        }
    }
}
