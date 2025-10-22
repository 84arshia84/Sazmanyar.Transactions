using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_transActionAndItsRelations_addWorkFlow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionExecutionRequests_ContractTypes_CotractTypeId",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropIndex(
                name: "IX_TransactionExecutionRequests_CotractTypeId",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.RenameColumn(
                name: "CotractTypeId",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                newName: "CurrentStageId");

            migrationBuilder.AddColumn<Guid>(
                name: "ContractTypeId",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CurrentStatusTitle",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalApprove",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastActionTitle",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FinePaymentMethodID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitOfMeasurementID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionExecutionRequests_ContractTypeId",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionRequestServiceExplanation_CurrencyID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionRequestServiceExplanation_FinePaymentMethodID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation",
                column: "FinePaymentMethodID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutionRequestServiceExplanation_Currency_CurrencyID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation",
                column: "CurrencyID",
                principalSchema: "TAM",
                principalTable: "Currency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutionRequestServiceExplanation_FinePaymentMethod_FinePaymentMethodID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation",
                column: "FinePaymentMethodID",
                principalSchema: "TAM",
                principalTable: "FinePaymentMethod",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionExecutionRequests_ContractTypes_ContractTypeId",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                column: "ContractTypeId",
                principalSchema: "TAM",
                principalTable: "ContractTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutionRequestServiceExplanation_Currency_CurrencyID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation");

            migrationBuilder.DropForeignKey(
                name: "FK_ExecutionRequestServiceExplanation_FinePaymentMethod_FinePaymentMethodID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionExecutionRequests_ContractTypes_ContractTypeId",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropIndex(
                name: "IX_TransactionExecutionRequests_ContractTypeId",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropIndex(
                name: "IX_ExecutionRequestServiceExplanation_CurrencyID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation");

            migrationBuilder.DropIndex(
                name: "IX_ExecutionRequestServiceExplanation_FinePaymentMethodID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation");

            migrationBuilder.DropColumn(
                name: "ContractTypeId",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropColumn(
                name: "CurrentStatusTitle",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropColumn(
                name: "IsFinalApprove",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropColumn(
                name: "LastActionTitle",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropColumn(
                name: "CurrencyID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation");

            migrationBuilder.DropColumn(
                name: "FinePaymentMethodID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurementID",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation");

            migrationBuilder.RenameColumn(
                name: "CurrentStageId",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                newName: "CotractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionExecutionRequests_CotractTypeId",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                column: "CotractTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionExecutionRequests_ContractTypes_CotractTypeId",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                column: "CotractTypeId",
                principalSchema: "TAM",
                principalTable: "ContractTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
