using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_transActionAndItsRelations_addProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "TAM",
                table: "TransactionExecutionRequests");
        }
    }
}
