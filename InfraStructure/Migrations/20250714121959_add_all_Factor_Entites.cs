using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_all_Factor_Entites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageOfChanges",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(8,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(7,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GoodJob_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(8,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(7,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractValue_Added_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(8,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(7,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractTax_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(8,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(7,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractInsurance_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(8,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(7,5)");

            migrationBuilder.CreateTable(
                name: "FactorFinancialDetailes",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorInsurance_Percent = table.Column<decimal>(type: "Decimal(8,5)", nullable: false),
                    FactorValue_Added_Percent = table.Column<decimal>(type: "Decimal(8,5)", nullable: false),
                    FactorTax_Percent = table.Column<decimal>(type: "Decimal(8,5)", nullable: false),
                    GoodJob_Percent = table.Column<decimal>(type: "Decimal(8,5)", nullable: false),
                    FactorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorFinancialDetailes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FactorForms",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorForms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FactorTimeProfiles",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FactorStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FactorEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FactorPeriod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FactorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorTimeProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FactorTypes",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Factors",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactorNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactorExpertID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequirementToCloseTheAccount = table.Column<bool>(type: "bit", nullable: false),
                    InsertFactorDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertFactorBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CurrentStageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentStatusTitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastActionTitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsFinalApprove = table.Column<bool>(type: "bit", nullable: true),
                    FactorTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationalunitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleOfOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreditSourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CorespondentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorespondentRealOrLegal = table.Column<bool>(type: "bit", nullable: false),
                    FactorTimeProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorFinancialDetaileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factors_CreditSource_CreditSourceID",
                        column: x => x.CreditSourceID,
                        principalSchema: "TAM",
                        principalTable: "CreditSource",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Factors_FactorFinancialDetailes_FactorFinancialDetaileId",
                        column: x => x.FactorFinancialDetaileId,
                        principalSchema: "TAM",
                        principalTable: "FactorFinancialDetailes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Factors_FactorForms_FactorFormId",
                        column: x => x.FactorFormId,
                        principalSchema: "TAM",
                        principalTable: "FactorForms",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Factors_FactorTimeProfiles_FactorTimeProfileId",
                        column: x => x.FactorTimeProfileId,
                        principalSchema: "TAM",
                        principalTable: "FactorTimeProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Factors_FactorTypes_FactorTypeId",
                        column: x => x.FactorTypeId,
                        principalSchema: "TAM",
                        principalTable: "FactorTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Factors_Organizationalunit_OrganizationalunitId",
                        column: x => x.OrganizationalunitId,
                        principalSchema: "TAM",
                        principalTable: "Organizationalunit",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Factors_RoleOfOrganizations_RoleOfOrganizationId",
                        column: x => x.RoleOfOrganizationId,
                        principalSchema: "TAM",
                        principalTable: "RoleOfOrganizations",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FactorNettingProcessItems",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Percentage = table.Column<decimal>(type: "Decimal(8,5)", nullable: false),
                    Amount = table.Column<decimal>(type: "Decimal(30,5)", nullable: false),
                    IsDeduction = table.Column<bool>(type: "bit", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FactorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorNettingProcessItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorNettingProcessItems_Factors_FactorId",
                        column: x => x.FactorId,
                        principalSchema: "TAM",
                        principalTable: "Factors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorPayments",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "Decimal(30,5)", nullable: false),
                    AccelerationRate = table.Column<decimal>(type: "Decimal(30,5)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HowToPayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorPayments_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "TAM",
                        principalTable: "Currency",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FactorPayments_Factors_FactorId",
                        column: x => x.FactorId,
                        principalSchema: "TAM",
                        principalTable: "Factors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactorPayments_HowToPay_HowToPayId",
                        column: x => x.HowToPayId,
                        principalSchema: "TAM",
                        principalTable: "HowToPay",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FactorServiceExplanations",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityReference = table.Column<int>(type: "int", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProposalID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExplanationType = table.Column<int>(type: "int", nullable: false),
                    UnitAmount = table.Column<decimal>(type: "Decimal(30,5)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "Decimal(30,5)", nullable: true),
                    AccelerationRate = table.Column<decimal>(type: "Decimal(30,5)", nullable: true),
                    Amount = table.Column<decimal>(type: "Decimal(30,5)", nullable: true),
                    IsSupplyList = table.Column<bool>(type: "bit", nullable: false),
                    CommodityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommodityName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    SupplyListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplyListName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    ActivityCenterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitOfMeasurementID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FactorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorServiceExplanations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorServiceExplanations_Activitycenters_ActivityCenterID",
                        column: x => x.ActivityCenterID,
                        principalSchema: "TAM",
                        principalTable: "Activitycenters",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FactorServiceExplanations_Currency_CurrencyID",
                        column: x => x.CurrencyID,
                        principalSchema: "TAM",
                        principalTable: "Currency",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FactorServiceExplanations_Factors_FactorId",
                        column: x => x.FactorId,
                        principalSchema: "TAM",
                        principalTable: "Factors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactorServiceExplanations_UnitOfMeasurement_FactorId",
                        column: x => x.FactorId,
                        principalSchema: "TAM",
                        principalTable: "UnitOfMeasurement",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FactorNettingProcessItems_FactorId",
                schema: "TAM",
                table: "FactorNettingProcessItems",
                column: "FactorId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorPayments_CurrencyId",
                schema: "TAM",
                table: "FactorPayments",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorPayments_FactorId",
                schema: "TAM",
                table: "FactorPayments",
                column: "FactorId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorPayments_HowToPayId",
                schema: "TAM",
                table: "FactorPayments",
                column: "HowToPayId");

            migrationBuilder.CreateIndex(
                name: "IX_Factors_CreditSourceID",
                schema: "TAM",
                table: "Factors",
                column: "CreditSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Factors_FactorFinancialDetaileId",
                schema: "TAM",
                table: "Factors",
                column: "FactorFinancialDetaileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factors_FactorFormId",
                schema: "TAM",
                table: "Factors",
                column: "FactorFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Factors_FactorTimeProfileId",
                schema: "TAM",
                table: "Factors",
                column: "FactorTimeProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factors_FactorTypeId",
                schema: "TAM",
                table: "Factors",
                column: "FactorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Factors_OrganizationalunitId",
                schema: "TAM",
                table: "Factors",
                column: "OrganizationalunitId");

            migrationBuilder.CreateIndex(
                name: "IX_Factors_RoleOfOrganizationId",
                schema: "TAM",
                table: "Factors",
                column: "RoleOfOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorServiceExplanations_ActivityCenterID",
                schema: "TAM",
                table: "FactorServiceExplanations",
                column: "ActivityCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_FactorServiceExplanations_CurrencyID",
                schema: "TAM",
                table: "FactorServiceExplanations",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_FactorServiceExplanations_FactorId",
                schema: "TAM",
                table: "FactorServiceExplanations",
                column: "FactorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactorNettingProcessItems",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorPayments",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorServiceExplanations",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "Factors",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorFinancialDetailes",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorForms",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorTimeProfiles",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorTypes",
                schema: "TAM");

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageOfChanges",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(7,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(8,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GoodJob_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(7,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(8,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractValue_Added_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(7,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(8,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractTax_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(7,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(8,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractInsurance_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(7,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(8,5)");
        }
    }
}
