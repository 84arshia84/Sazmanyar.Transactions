using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_transActionAndItsRelations_addPropertyAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InsertBy",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertBy",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropColumn(
                name: "InsertDate",
                schema: "TAM",
                table: "TransactionExecutionRequests");
        }
    }
}
