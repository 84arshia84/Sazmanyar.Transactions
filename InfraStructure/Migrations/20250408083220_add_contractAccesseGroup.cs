using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_contractAccesseGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractAccessGroups",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessGroupPropertiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessGroupPermissionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAccessGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractAccessGroupContractTypes",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAccessGroupContractTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAccessGroupContractTypes_ContractAccessGroups_ContractAccessGroupId",
                        column: x => x.ContractAccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "ContractAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractAccessGroupContractTypes_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalSchema: "TAM",
                        principalTable: "ContractTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractAccessGroupGroups",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAccessGroupGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAccessGroupGroups_ContractAccessGroups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "TAM",
                        principalTable: "ContractAccessGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractAccessGroupGroups_ContractAccessGroups_ParentGroupId",
                        column: x => x.ParentGroupId,
                        principalSchema: "TAM",
                        principalTable: "ContractAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractAccessGroupOrganizationUnits",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAccessGroupOrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAccessGroupOrganizationUnits_ContractAccessGroups_ContractAccessGroupId",
                        column: x => x.ContractAccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "ContractAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractAccessGroupOrganizationUnits_Organizationalunit_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalSchema: "TAM",
                        principalTable: "Organizationalunit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractAccessGroupPermissions",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Contract = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ContractAddendum = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Settings = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AccessGroupSettings = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    BaseSettings = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PriceListSettings = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    WorkFlow = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ContractAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAccessGroupPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAccessGroupPermissions_ContractAccessGroups_ContractAccessGroupId",
                        column: x => x.ContractAccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "ContractAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractAccessGroupProperties",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractTypeView = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ContractTypeEdit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ContractTypeDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RoleOfOrganizationView = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RoleOfOrganizationEdit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RoleOfOrganizationDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OrganizationUnitView = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OrganizationUnitEdit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OrganizationUnitDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ContractAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAccessGroupProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAccessGroupProperties_ContractAccessGroups_ContractAccessGroupId",
                        column: x => x.ContractAccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "ContractAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractAccessGroupRoleOfOrganizations",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleOfOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAccessGroupRoleOfOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAccessGroupRoleOfOrganizations_ContractAccessGroups_ContractAccessGroupId",
                        column: x => x.ContractAccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "ContractAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractAccessGroupRoleOfOrganizations_RoleOfOrganizations_RoleOfOrganizationId",
                        column: x => x.RoleOfOrganizationId,
                        principalSchema: "TAM",
                        principalTable: "RoleOfOrganizations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractAccessGroupUsers",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAccessGroupUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAccessGroupUsers_ContractAccessGroups_AccessGroupId",
                        column: x => x.AccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "ContractAccessGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupContractTypes_ContractAccessGroupId",
                schema: "TAM",
                table: "ContractAccessGroupContractTypes",
                column: "ContractAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupContractTypes_ContractTypeId",
                schema: "TAM",
                table: "ContractAccessGroupContractTypes",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupGroups_GroupId",
                schema: "TAM",
                table: "ContractAccessGroupGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupGroups_ParentGroupId",
                schema: "TAM",
                table: "ContractAccessGroupGroups",
                column: "ParentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupOrganizationUnits_ContractAccessGroupId",
                schema: "TAM",
                table: "ContractAccessGroupOrganizationUnits",
                column: "ContractAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupOrganizationUnits_OrganizationUnitId",
                schema: "TAM",
                table: "ContractAccessGroupOrganizationUnits",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupPermissions_ContractAccessGroupId",
                schema: "TAM",
                table: "ContractAccessGroupPermissions",
                column: "ContractAccessGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupProperties_ContractAccessGroupId",
                schema: "TAM",
                table: "ContractAccessGroupProperties",
                column: "ContractAccessGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupRoleOfOrganizations_ContractAccessGroupId",
                schema: "TAM",
                table: "ContractAccessGroupRoleOfOrganizations",
                column: "ContractAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupRoleOfOrganizations_RoleOfOrganizationId",
                schema: "TAM",
                table: "ContractAccessGroupRoleOfOrganizations",
                column: "RoleOfOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAccessGroupUsers_AccessGroupId",
                schema: "TAM",
                table: "ContractAccessGroupUsers",
                column: "AccessGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractAccessGroupContractTypes",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractAccessGroupGroups",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractAccessGroupOrganizationUnits",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractAccessGroupPermissions",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractAccessGroupProperties",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractAccessGroupRoleOfOrganizations",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractAccessGroupUsers",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractAccessGroups",
                schema: "TAM");
        }
    }
}
