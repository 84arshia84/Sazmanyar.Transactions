using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_designCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractDesignCode",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterPreview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDesignCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FactorDesignCode",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterPreview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorDesignCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDesignCode",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterPreview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDesignCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransActionExecutionDesignCode",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterPreview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransActionExecutionDesignCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractDesignCodeParameterRel",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleOfOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractDesignCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDesignCodeParameterRel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractDesignCodeParameterRel_ContractDesignCode_ContractDesignCodeId",
                        column: x => x.ContractDesignCodeId,
                        principalSchema: "TAM",
                        principalTable: "ContractDesignCode",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FactorDesignCodeParameterRel",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleOfOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FactorDesignCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorDesignCodeParameterRel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorDesignCodeParameterRel_FactorDesignCode_FactorDesignCodeId",
                        column: x => x.FactorDesignCodeId,
                        principalSchema: "TAM",
                        principalTable: "FactorDesignCode",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDesignCodeParameterRel",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleOfOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoiceDesignCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDesignCodeParameterRel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDesignCodeParameterRel_InvoiceDesignCode_InvoiceDesignCodeId",
                        column: x => x.InvoiceDesignCodeId,
                        principalSchema: "TAM",
                        principalTable: "InvoiceDesignCode",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransActionExecutionDesignCodeParameterRel",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransactionExecutionDesignCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransActionExecutionDesignCodeParameterRel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransActionExecutionDesignCodeParameterRel_TransActionExecutionDesignCode_TransactionExecutionDesignCodeId",
                        column: x => x.TransactionExecutionDesignCodeId,
                        principalSchema: "TAM",
                        principalTable: "TransActionExecutionDesignCode",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractDesignCodeParameterRel_ContractDesignCodeId",
                schema: "TAM",
                table: "ContractDesignCodeParameterRel",
                column: "ContractDesignCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorDesignCodeParameterRel_FactorDesignCodeId",
                schema: "TAM",
                table: "FactorDesignCodeParameterRel",
                column: "FactorDesignCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDesignCodeParameterRel_InvoiceDesignCodeId",
                schema: "TAM",
                table: "InvoiceDesignCodeParameterRel",
                column: "InvoiceDesignCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransActionExecutionDesignCodeParameterRel_TransactionExecutionDesignCodeId",
                schema: "TAM",
                table: "TransActionExecutionDesignCodeParameterRel",
                column: "TransactionExecutionDesignCodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractDesignCodeParameterRel",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorDesignCodeParameterRel",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "InvoiceDesignCodeParameterRel",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "TransActionExecutionDesignCodeParameterRel",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractDesignCode",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorDesignCode",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "InvoiceDesignCode",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "TransActionExecutionDesignCode",
                schema: "TAM");
        }
    }
}
