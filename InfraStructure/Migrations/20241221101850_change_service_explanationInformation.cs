using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class change_service_explanationInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractEstimatedmeters_Contracts_ContractID",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropIndex(
                name: "IX_ContractEstimatedmeters_ContractID",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.DropColumn(
                name: "ContractID",
                schema: "TAM",
                table: "ContractEstimatedmeters");

            migrationBuilder.CreateTable(
                name: "ServiceExplanationFinancials",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedVolume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedPercent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceBaseInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceExplanationFinancials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceExplanationFinancials_InvoiceBaseInformations_InvoiceBaseInformationId",
                        column: x => x.InvoiceBaseInformationId,
                        principalSchema: "TAM",
                        principalTable: "InvoiceBaseInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanationFinancials_InvoiceBaseInformationId",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                column: "InvoiceBaseInformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceExplanationFinancials",
                schema: "TAM");

            migrationBuilder.AddColumn<Guid>(
                name: "ContractID",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractEstimatedmeters_ContractID",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEstimatedmeters_Contracts_ContractID",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                column: "ContractID",
                principalSchema: "TAM",
                principalTable: "Contracts",
                principalColumn: "ID");
        }
    }
}
