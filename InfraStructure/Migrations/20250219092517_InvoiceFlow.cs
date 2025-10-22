using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceFlow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentStageId",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CurrentStateTitle",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalApproved",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastActionTitle",
                schema: "TAM",
                table: "InvoiceBaseInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStageId",
                schema: "TAM",
                table: "InvoiceBaseInformations");

            migrationBuilder.DropColumn(
                name: "CurrentStateTitle",
                schema: "TAM",
                table: "InvoiceBaseInformations");

            migrationBuilder.DropColumn(
                name: "IsFinalApproved",
                schema: "TAM",
                table: "InvoiceBaseInformations");

            migrationBuilder.DropColumn(
                name: "LastActionTitle",
                schema: "TAM",
                table: "InvoiceBaseInformations");
        }
    }
}
