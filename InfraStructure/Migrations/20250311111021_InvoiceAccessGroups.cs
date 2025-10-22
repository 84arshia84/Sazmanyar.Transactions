using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceAccessGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceAccessGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceAccessGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessGroupProperties",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceTypeView = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceTypeEdit = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceTypeDelete = table.Column<bool>(type: "bit", nullable: false),
                    ContractTypeView = table.Column<bool>(type: "bit", nullable: false),
                    ContractTypeEdit = table.Column<bool>(type: "bit", nullable: false),
                    ContractTypeDelete = table.Column<bool>(type: "bit", nullable: false),
                    RoleOfOrganizationView = table.Column<bool>(type: "bit", nullable: false),
                    RoleOfOrganizationEdit = table.Column<bool>(type: "bit", nullable: false),
                    RoleOfOrganizationDelete = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationUnitView = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationUnitEdit = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationUnitDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessGroupProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessGroupProperties_InvoiceAccessGroups_InvoiceAccessGroupId",
                        column: x => x.InvoiceAccessGroupId,
                        principalTable: "InvoiceAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceAccessGroupContractTypes",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceAccessGroupContractTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceAccessGroupContractTypes_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalSchema: "TAM",
                        principalTable: "ContractTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceAccessGroupContractTypes_InvoiceAccessGroups_InvoiceAccessGroupId",
                        column: x => x.InvoiceAccessGroupId,
                        principalTable: "InvoiceAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceAccessGroupGroups",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessGroupParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceAccessGroupGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupId",
                        column: x => x.AccessGroupId,
                        principalTable: "InvoiceAccessGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupParentId",
                        column: x => x.AccessGroupParentId,
                        principalTable: "InvoiceAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceAccessGroupInvoiceTypes",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceAccessGroupInvoiceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceAccessGroupInvoiceTypes_InvoiceAccessGroups_InvoiceAccessGroupId",
                        column: x => x.InvoiceAccessGroupId,
                        principalTable: "InvoiceAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceAccessGroupInvoiceTypes_InvoiceTypes_InvoiceTypeId",
                        column: x => x.InvoiceTypeId,
                        principalSchema: "TAM",
                        principalTable: "InvoiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceAccessGroupOrganizationUnits",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationalUnitID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceAccessGroupOrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceAccessGroupOrganizationUnits_InvoiceAccessGroups_InvoiceAccessGroupId",
                        column: x => x.InvoiceAccessGroupId,
                        principalTable: "InvoiceAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceAccessGroupOrganizationUnits_Organizationalunit_OrganizationalUnitID",
                        column: x => x.OrganizationalUnitID,
                        principalSchema: "TAM",
                        principalTable: "Organizationalunit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceAccessGroupPermissions",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Invoice = table.Column<bool>(type: "bit", nullable: false),
                    Settings = table.Column<bool>(type: "bit", nullable: false),
                    AccessGroupSettings = table.Column<bool>(type: "bit", nullable: false),
                    BaseSettings = table.Column<bool>(type: "bit", nullable: false),
                    WorkFlow = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceAccessGroupPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceAccessGroupPermissions_InvoiceAccessGroups_InvoiceAccessGroupId",
                        column: x => x.InvoiceAccessGroupId,
                        principalTable: "InvoiceAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceAccessGroupRoleOfOrganizations",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleOfOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceAccessGroupRoleOfOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceAccessGroupRoleOfOrganizations_InvoiceAccessGroups_InvoiceAccessGroupId",
                        column: x => x.InvoiceAccessGroupId,
                        principalTable: "InvoiceAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceAccessGroupRoleOfOrganizations_RoleOfOrganizations_RoleOfOrganizationId",
                        column: x => x.RoleOfOrganizationId,
                        principalSchema: "TAM",
                        principalTable: "RoleOfOrganizations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceAccessGroupUsers",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceAccessGroupUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceAccessGroupUsers_InvoiceAccessGroups_AccessGroupId",
                        column: x => x.AccessGroupId,
                        principalTable: "InvoiceAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessGroupProperties_InvoiceAccessGroupId",
                schema: "TAM",
                table: "AccessGroupProperties",
                column: "InvoiceAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupContractTypes_ContractTypeId",
                schema: "TAM",
                table: "InvoiceAccessGroupContractTypes",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupContractTypes_InvoiceAccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupContractTypes",
                column: "InvoiceAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupGroups_AccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "AccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupGroups_AccessGroupParentId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "AccessGroupParentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupGroups_GroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupGroups_ParentGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "ParentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupInvoiceTypes_InvoiceAccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupInvoiceTypes",
                column: "InvoiceAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupInvoiceTypes_InvoiceTypeId",
                schema: "TAM",
                table: "InvoiceAccessGroupInvoiceTypes",
                column: "InvoiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupOrganizationUnits_InvoiceAccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupOrganizationUnits",
                column: "InvoiceAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupOrganizationUnits_OrganizationalUnitID",
                schema: "TAM",
                table: "InvoiceAccessGroupOrganizationUnits",
                column: "OrganizationalUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupOrganizationUnits_OrganizationUnitId",
                schema: "TAM",
                table: "InvoiceAccessGroupOrganizationUnits",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupPermissions_InvoiceAccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupPermissions",
                column: "InvoiceAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupRoleOfOrganizations_InvoiceAccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupRoleOfOrganizations",
                column: "InvoiceAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupRoleOfOrganizations_RoleOfOrganizationId",
                schema: "TAM",
                table: "InvoiceAccessGroupRoleOfOrganizations",
                column: "RoleOfOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupUsers_AccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupUsers",
                column: "AccessGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessGroupProperties",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "InvoiceAccessGroupContractTypes",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "InvoiceAccessGroupGroups",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "InvoiceAccessGroupInvoiceTypes",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "InvoiceAccessGroupOrganizationUnits",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "InvoiceAccessGroupPermissions",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "InvoiceAccessGroupRoleOfOrganizations",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "InvoiceAccessGroupUsers",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "InvoiceAccessGroups");
        }
    }
}
