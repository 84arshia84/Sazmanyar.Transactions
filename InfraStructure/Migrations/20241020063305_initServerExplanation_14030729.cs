using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class initServerExplanation_14030729 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContractExpertName",
                schema: "TAM",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractExpertID",
                schema: "TAM",
                table: "Contracts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractDateOfNotification",
                schema: "TAM",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractEndDate",
                schema: "TAM",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractExchangeDate",
                schema: "TAM",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractPeriod",
                schema: "TAM",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractStartDate",
                schema: "TAM",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasurement",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasurement", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ServiceExplanation",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tilte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityReference = table.Column<int>(type: "int", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProposalID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProposalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExplanationAmount = table.Column<long>(type: "bigint", nullable: false),
                    AccelerationRate = table.Column<int>(type: "int", nullable: false),
                    PrepaymentPercentage = table.Column<int>(type: "int", nullable: false),
                    ExplanationStartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExplanationEndingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExplanationType = table.Column<int>(type: "int", nullable: false),
                    ActivityCenterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinePaymentMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitOfMeasurementID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceExplanation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServiceExplanation_Activitycenters_ActivityCenterID",
                        column: x => x.ActivityCenterID,
                        principalSchema: "TAM",
                        principalTable: "Activitycenters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceExplanation_Contracts_ContractID",
                        column: x => x.ContractID,
                        principalSchema: "TAM",
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceExplanation_Currency_CurrencyID",
                        column: x => x.CurrencyID,
                        principalSchema: "TAM",
                        principalTable: "Currency",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceExplanation_FinePaymentMethod_FinePaymentMethodID",
                        column: x => x.FinePaymentMethodID,
                        principalSchema: "TAM",
                        principalTable: "FinePaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceExplanation_UnitOfMeasurement_UnitOfMeasurementID",
                        column: x => x.UnitOfMeasurementID,
                        principalSchema: "TAM",
                        principalTable: "UnitOfMeasurement",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_ActivityCenterID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "ActivityCenterID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_ContractID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "ContractID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceExplanation",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "UnitOfMeasurement",
                schema: "TAM");

            migrationBuilder.DropColumn(
                name: "ContractDateOfNotification",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractEndDate",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractExchangeDate",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractPeriod",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractStartDate",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.AlterColumn<string>(
                name: "ContractExpertName",
                schema: "TAM",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractExpertID",
                schema: "TAM",
                table: "Contracts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
