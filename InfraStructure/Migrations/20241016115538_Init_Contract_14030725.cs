using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class Init_Contract_14030725 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractFinancialDetails",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractInsurance_Percent = table.Column<long>(type: "bigint", nullable: false),
                    ContractValue_Added_Percent = table.Column<long>(type: "bigint", nullable: false),
                    ContractTax_Percent = table.Column<long>(type: "bigint", nullable: false),
                    PercentageOfChanges = table.Column<long>(type: "bigint", nullable: false),
                    GoodJob_Percent = table.Column<long>(type: "bigint", nullable: false),
                    Amount_of_timeExtension = table.Column<long>(type: "bigint", nullable: false),
                    Basis_of_Receipt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractFinancialDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContractTimeProfiles",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractDateOfNotification = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractExchangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractPeriod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BasisForStartingProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BasisForStartingTheProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTimeProfiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContractTimeProfiles_BasisForStartingTheProject_BasisForStartingTheProjectID",
                        column: x => x.BasisForStartingTheProjectID,
                        principalSchema: "TAM",
                        principalTable: "BasisForStartingTheProject",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractExpertID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractExpertName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsultantID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConsultantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequirementToCloseTheAccount = table.Column<bool>(type: "bit", nullable: false),
                    InsertContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertContractBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ContractTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransActionTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleOFOrganizationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorespondentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationUnitID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreditSourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeProfileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialDetailsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractFinancialDetails_FinancialDetailsID",
                        column: x => x.FinancialDetailsID,
                        principalSchema: "TAM",
                        principalTable: "ContractFinancialDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractTimeProfiles_TimeProfileID",
                        column: x => x.TimeProfileID,
                        principalSchema: "TAM",
                        principalTable: "ContractTimeProfiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_CreditSource_CreditSourceID",
                        column: x => x.CreditSourceID,
                        principalSchema: "TAM",
                        principalTable: "CreditSource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Organizationalunit_OrganizationUnitID",
                        column: x => x.OrganizationUnitID,
                        principalSchema: "TAM",
                        principalTable: "Organizationalunit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_TransActionTypes_TransActionTypeID",
                        column: x => x.TransActionTypeID,
                        principalSchema: "TAM",
                        principalTable: "TransActionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CreditSourceID",
                schema: "TAM",
                table: "Contracts",
                column: "CreditSourceID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_FinancialDetailsID",
                schema: "TAM",
                table: "Contracts",
                column: "FinancialDetailsID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OrganizationUnitID",
                schema: "TAM",
                table: "Contracts",
                column: "OrganizationUnitID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TimeProfileID",
                schema: "TAM",
                table: "Contracts",
                column: "TimeProfileID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TransActionTypeID",
                schema: "TAM",
                table: "Contracts",
                column: "TransActionTypeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractTimeProfiles_BasisForStartingTheProjectID",
                schema: "TAM",
                table: "ContractTimeProfiles",
                column: "BasisForStartingTheProjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractFinancialDetails",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractTimeProfiles",
                schema: "TAM");
        }
    }
}
