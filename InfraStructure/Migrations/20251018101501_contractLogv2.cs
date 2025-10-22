using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class contractLogv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractLogs",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransActionTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleOFOrganizationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorespondentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorespondentRealOrLegal = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationUnitID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreditSourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TimeProfileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialDetailsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractCheckListValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractExpertID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractExpertName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsultantID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConsultantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequirementToCloseTheAccount = table.Column<bool>(type: "bit", nullable: false),
                    InsertContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertContractBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    HasAddendum = table.Column<bool>(type: "bit", nullable: true),
                    CurrentStageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentStatusTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastActionTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFinalApprove = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangesSummary = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractLogs_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "TAM",
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractLogs_ContractId",
                schema: "TAM",
                table: "ContractLogs",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractLogs",
                schema: "TAM");
        }
    }
}
