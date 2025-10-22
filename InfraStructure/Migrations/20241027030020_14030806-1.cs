using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class _140308061 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceExplanation_ActivityCenterID",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropIndex(
                name: "IX_ServiceExplanation_CurrencyID",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropIndex(
                name: "IX_ServiceExplanation_FinePaymentMethodID",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropIndex(
                name: "IX_ServiceExplanation_UnitOfMeasurementID",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverablePen",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_ActivityCenterID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "ActivityCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_CurrencyID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_FinePaymentMethodID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "FinePaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_UnitOfMeasurementID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "UnitOfMeasurementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceExplanation_ActivityCenterID",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropIndex(
                name: "IX_ServiceExplanation_CurrencyID",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropIndex(
                name: "IX_ServiceExplanation_FinePaymentMethodID",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropIndex(
                name: "IX_ServiceExplanation_UnitOfMeasurementID",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropColumn(
                name: "DeliverablePen",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_ActivityCenterID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "ActivityCenterID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_CurrencyID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "CurrencyID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_FinePaymentMethodID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "FinePaymentMethodID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_UnitOfMeasurementID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "UnitOfMeasurementID",
                unique: true,
                filter: "[UnitOfMeasurementID] IS NOT NULL");
        }
    }
}
