using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class contractEstimatedMeter_relationWith_priceList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ContractEstimatedmeters_ClauseId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ClauseId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractEstimatedmeters_ExplenationId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ExplenationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractEstimatedmeters_FieldId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractEstimatedmeters_YearId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "YearId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEstimatedmeters_PriceListClause_ClauseId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ClauseId",
                principalSchema: "TAM",
                principalTable: "PriceListClause",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEstimatedmeters_PriceListExplanation_ExplenationId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ExplenationId",
                principalSchema: "TAM",
                principalTable: "PriceListExplanation",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEstimatedmeters_PriceListField_FieldId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "FieldId",
                principalSchema: "TAM",
                principalTable: "PriceListField",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEstimatedmeters_PriceList_YearId",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "YearId",
                principalSchema: "TAM",
                principalTable: "PriceList",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractEstimatedmeters_PriceListClause_ClauseId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractEstimatedmeters_PriceListExplanation_ExplenationId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractEstimatedmeters_PriceListField_FieldId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractEstimatedmeters_PriceList_YearId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropIndex(
                name: "IX_ContractEstimatedmeters_ClauseId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropIndex(
                name: "IX_ContractEstimatedmeters_ExplenationId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropIndex(
                name: "IX_ContractEstimatedmeters_FieldId",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropIndex(
                name: "IX_ContractEstimatedmeters_YearId",
                schema: "TAM",
                table: "ContractEstimatedmeters");
        }
    }
}
