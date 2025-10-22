using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class addOrderToEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "UnitOfMeasurement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "TypeOfCooperation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "TransActionTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "ReleaseCondition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "ReasonForTermination",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "ReasonForCancellation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "PaymentMethods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "Organizationalunit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "Monetaryunit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "HowToPay",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "ForGuarantee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "FinePaymentMethod",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "ExtensionType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "Currency",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "CreditSource",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "BasisFortheEndOftheProject",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "BasisForStartingTheProject",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "AddendumTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "TAM",
                table: "Activitycenters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EstimatedMeterFinancials",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractEstimatedmeterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedVolume = table.Column<decimal>(type: "decimal(30,5)", nullable: true),
                    RequestedPercent = table.Column<decimal>(type: "decimal(7,5)", nullable: false),
                    RequestedPrice = table.Column<decimal>(type: "decimal(30,5)", nullable: false),
                    ApprovedVolume = table.Column<decimal>(type: "decimal(30,5)", nullable: true),
                    ApprovedPercent = table.Column<decimal>(type: "decimal(7,5)", nullable: false),
                    ApprovedPrice = table.Column<decimal>(type: "decimal(30,5)", nullable: false),
                    InvoiceBaseInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstimatedMeterFinancials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstimatedMeterFinancials_InvoiceBaseInformations_InvoiceBaseInformationId",
                        column: x => x.InvoiceBaseInformationId,
                        principalSchema: "TAM",
                        principalTable: "InvoiceBaseInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstimatedMeterFinancials_InvoiceBaseInformationId",
                schema: "TAM",
                table: "EstimatedMeterFinancials",
                column: "InvoiceBaseInformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstimatedMeterFinancials",
                schema: "TAM");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "UnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "TypeOfCooperation");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "TransActionTypes");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "ReleaseCondition");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "ReasonForTermination");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "ReasonForCancellation");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "Organizationalunit");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "Monetaryunit");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "HowToPay");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "ForGuarantee");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "FinePaymentMethod");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "ExtensionType");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "CreditSource");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "BasisFortheEndOftheProject");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "BasisForStartingTheProject");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "AddendumTypes");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "TAM",
                table: "Activitycenters");
        }
    }
}
