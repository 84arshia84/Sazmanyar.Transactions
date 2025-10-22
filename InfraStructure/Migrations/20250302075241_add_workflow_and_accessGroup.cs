using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_workflow_and_accessGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContractWorkFlowId",
                schema: "TAM",
                table: "Contracts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContractAndAddendumAccessGroup",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAndAddendumAccessGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractWorkFlow",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentStageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentStatusTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastActionTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractWorkFlow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractWorkFlowHistory",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionerName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ActionerFullQualifyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PerformedAction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractWorkFlowHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractWorkFlowHistory_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "TAM",
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypeAndCAAGRel",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CAAGId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypeAndCAAGRel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractTypeAndCAAGRel_ContractAndAddendumAccessGroup_CAAGId",
                        column: x => x.CAAGId,
                        principalSchema: "TAM",
                        principalTable: "ContractAndAddendumAccessGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractTypeAndCAAGRel_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalSchema: "TAM",
                        principalTable: "ContractTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalunitAndCAAGRel",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CAAGId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationalunitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalunitAndCAAGRel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationalunitAndCAAGRel_ContractAndAddendumAccessGroup_CAAGId",
                        column: x => x.CAAGId,
                        principalSchema: "TAM",
                        principalTable: "ContractAndAddendumAccessGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationalunitAndCAAGRel_Organizationalunit_OrganizationalunitId",
                        column: x => x.OrganizationalunitId,
                        principalSchema: "TAM",
                        principalTable: "Organizationalunit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleOFOrganizationAndCAAGRel",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CAAGId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleOfOranizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleOfOrganizationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleOFOrganizationAndCAAGRel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleOFOrganizationAndCAAGRel_ContractAndAddendumAccessGroup_CAAGId",
                        column: x => x.CAAGId,
                        principalSchema: "TAM",
                        principalTable: "ContractAndAddendumAccessGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleOFOrganizationAndCAAGRel_RoleOfOrganizations_RoleOfOrganizationID",
                        column: x => x.RoleOfOrganizationID,
                        principalSchema: "TAM",
                        principalTable: "RoleOfOrganizations",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UsersAndCAAGRel",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ACCAGId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFullQuallifyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAndCAAGRel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersAndCAAGRel_ContractAndAddendumAccessGroup_ACCAGId",
                        column: x => x.ACCAGId,
                        principalSchema: "TAM",
                        principalTable: "ContractAndAddendumAccessGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractWorkFlowId",
                schema: "TAM",
                table: "Contracts",
                column: "ContractWorkFlowId",
                unique: true,
                filter: "[ContractWorkFlowId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeAndCAAGRel_CAAGId",
                schema: "TAM",
                table: "ContractTypeAndCAAGRel",
                column: "CAAGId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeAndCAAGRel_ContractTypeId",
                schema: "TAM",
                table: "ContractTypeAndCAAGRel",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractWorkFlowHistory_ContractId",
                schema: "TAM",
                table: "ContractWorkFlowHistory",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalunitAndCAAGRel_CAAGId",
                schema: "TAM",
                table: "OrganizationalunitAndCAAGRel",
                column: "CAAGId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalunitAndCAAGRel_OrganizationalunitId",
                schema: "TAM",
                table: "OrganizationalunitAndCAAGRel",
                column: "OrganizationalunitId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleOFOrganizationAndCAAGRel_CAAGId",
                schema: "TAM",
                table: "RoleOFOrganizationAndCAAGRel",
                column: "CAAGId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleOFOrganizationAndCAAGRel_RoleOfOrganizationID",
                schema: "TAM",
                table: "RoleOFOrganizationAndCAAGRel",
                column: "RoleOfOrganizationID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAndCAAGRel_ACCAGId",
                schema: "TAM",
                table: "UsersAndCAAGRel",
                column: "ACCAGId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ContractWorkFlow_ContractWorkFlowId",
                schema: "TAM",
                table: "Contracts",
                column: "ContractWorkFlowId",
                principalSchema: "TAM",
                principalTable: "ContractWorkFlow",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ContractWorkFlow_ContractWorkFlowId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "ContractTypeAndCAAGRel",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractWorkFlow",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractWorkFlowHistory",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "OrganizationalunitAndCAAGRel",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "RoleOFOrganizationAndCAAGRel",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "UsersAndCAAGRel",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractAndAddendumAccessGroup",
                schema: "TAM");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ContractWorkFlowId",
                schema: "TAM",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractWorkFlowId",
                schema: "TAM",
                table: "Contracts");
        }
    }
}
