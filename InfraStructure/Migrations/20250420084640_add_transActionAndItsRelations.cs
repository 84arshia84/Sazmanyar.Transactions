using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_transActionAndItsRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsForExecutionRequest",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransactionExecutionRequests",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectOfRequest = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DateOfRequest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfRequest = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstimatedTimeForDoingRequest = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    ExplenationRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExplenationRequestOfConsultant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CotractTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationalunitID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExecutionRequestCheckListValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionExecutionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionExecutionRequests_ContractTypes_CotractTypeId",
                        column: x => x.CotractTypeId,
                        principalSchema: "TAM",
                        principalTable: "ContractTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionExecutionRequests_Organizationalunit_OrganizationalunitID",
                        column: x => x.OrganizationalunitID,
                        principalSchema: "TAM",
                        principalTable: "Organizationalunit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExecutionRequestCheckListValue",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckListValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionExecutionRequestsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionRequestCheckListValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutionRequestCheckListValue_TransactionExecutionRequests_TransactionExecutionRequestsId",
                        column: x => x.TransactionExecutionRequestsId,
                        principalSchema: "TAM",
                        principalTable: "TransactionExecutionRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExecutionRequestServiceExplanation",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityReference = table.Column<int>(type: "int", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProposalID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProposalName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UnitAmount = table.Column<decimal>(type: "Decimal(30,5)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "Decimal(30,5)", nullable: true),
                    AccelerationRate = table.Column<decimal>(type: "Decimal(10,5)", nullable: true),
                    PrepaymentPercentage = table.Column<decimal>(type: "Decimal(7,5)", nullable: false),
                    ExplanationStartingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExplanationEndingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExplanationType = table.Column<int>(type: "int", nullable: false),
                    ProgramVolume = table.Column<decimal>(type: "Decimal(30,5)", nullable: true),
                    DeliverablePen = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Order = table.Column<long>(type: "bigint", nullable: false),
                    IsSupplyList = table.Column<bool>(type: "bit", nullable: false),
                    CommodityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommodityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SupplyListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplyListName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActivityCenterTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ActivityCenterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionExecutionRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionRequestServiceExplanation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExecutionRequestServiceExplanation_TransactionExecutionRequests_TransactionExecutionRequestId",
                        column: x => x.TransactionExecutionRequestId,
                        principalSchema: "TAM",
                        principalTable: "TransactionExecutionRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionRequestCheckListValue_TransactionExecutionRequestsId",
                schema: "TAM",
                table: "ExecutionRequestCheckListValue",
                column: "TransactionExecutionRequestsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionRequestServiceExplanation_TransactionExecutionRequestId",
                schema: "TAM",
                table: "ExecutionRequestServiceExplanation",
                column: "TransactionExecutionRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionExecutionRequests_CotractTypeId",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                column: "CotractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionExecutionRequests_OrganizationalunitID",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                column: "OrganizationalunitID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExecutionRequestCheckListValue",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ExecutionRequestServiceExplanation",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "TransactionExecutionRequests",
                schema: "TAM");

            migrationBuilder.DropColumn(
                name: "IsForExecutionRequest",
                schema: "TAM",
                table: "ServiceExplanation");
        }
    }
}
