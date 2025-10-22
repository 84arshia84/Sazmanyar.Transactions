using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_accessGroupOfFactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliverablePen",
                schema: "TAM",
                table: "FactorServiceExplanations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NettingProcessTypes",
                schema: "TAM",
                table: "FactorNettingProcessItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FactorAccessGroup",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessGroupPropertiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessGroupPermissionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorAccessGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FactorAccessGroupFactorType",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorAccessGroupFactorType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorAccessGroupFactorType_FactorAccessGroup_FactorAccessGroupId",
                        column: x => x.FactorAccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "FactorAccessGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactorAccessGroupFactorType_FactorTypes_FactorTypeId",
                        column: x => x.FactorTypeId,
                        principalSchema: "TAM",
                        principalTable: "FactorTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorAccessGroupGroups",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorAccessGroupGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorAccessGroupGroups_FactorAccessGroup_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "TAM",
                        principalTable: "FactorAccessGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FactorAccessGroupGroups_FactorAccessGroup_ParentGroupId",
                        column: x => x.ParentGroupId,
                        principalSchema: "TAM",
                        principalTable: "FactorAccessGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorAccessGroupOrganizationUnits",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorAccessGroupOrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorAccessGroupOrganizationUnits_FactorAccessGroup_FactorAccessGroupId",
                        column: x => x.FactorAccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "FactorAccessGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactorAccessGroupOrganizationUnits_Organizationalunit_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalSchema: "TAM",
                        principalTable: "Organizationalunit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorAccessGroupPermissions",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Factor = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AccessGroupSettings = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    BaseSettings = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    WorkFlow = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FactorAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorAccessGroupPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorAccessGroupPermissions_FactorAccessGroup_FactorAccessGroupId",
                        column: x => x.FactorAccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "FactorAccessGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorAccessGroupProperties",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorTypeSave = table.Column<bool>(type: "bit", nullable: false),
                    FactorTypeView = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FactorTypeEdit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FactorTypeDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RoleOfOrganizationSave = table.Column<bool>(type: "bit", nullable: false),
                    RoleOfOrganizationView = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RoleOfOrganizationEdit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RoleOfOrganizationDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OrganizationUnitSave = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationUnitView = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OrganizationUnitEdit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OrganizationUnitDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FactorAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorAccessGroupProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorAccessGroupProperties_FactorAccessGroup_FactorAccessGroupId",
                        column: x => x.FactorAccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "FactorAccessGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorAccessGroupRoleOfOrganizations",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleOfOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorAccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorAccessGroupRoleOfOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorAccessGroupRoleOfOrganizations_FactorAccessGroup_FactorAccessGroupId",
                        column: x => x.FactorAccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "FactorAccessGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactorAccessGroupRoleOfOrganizations_RoleOfOrganizations_RoleOfOrganizationId",
                        column: x => x.RoleOfOrganizationId,
                        principalSchema: "TAM",
                        principalTable: "RoleOfOrganizations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorAccessGroupUsers",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorAccessGroupUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorAccessGroupUsers_FactorAccessGroup_AccessGroupId",
                        column: x => x.AccessGroupId,
                        principalSchema: "TAM",
                        principalTable: "FactorAccessGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FactorAccessGroupFactorType_FactorAccessGroupId",
                schema: "TAM",
                table: "FactorAccessGroupFactorType",
                column: "FactorAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorAccessGroupFactorType_FactorTypeId",
                schema: "TAM",
                table: "FactorAccessGroupFactorType",
                column: "FactorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorAccessGroupGroups_GroupId",
                schema: "TAM",
                table: "FactorAccessGroupGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorAccessGroupGroups_ParentGroupId",
                schema: "TAM",
                table: "FactorAccessGroupGroups",
                column: "ParentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorAccessGroupOrganizationUnits_FactorAccessGroupId",
                schema: "TAM",
                table: "FactorAccessGroupOrganizationUnits",
                column: "FactorAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorAccessGroupOrganizationUnits_OrganizationUnitId",
                schema: "TAM",
                table: "FactorAccessGroupOrganizationUnits",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorAccessGroupPermissions_FactorAccessGroupId",
                schema: "TAM",
                table: "FactorAccessGroupPermissions",
                column: "FactorAccessGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FactorAccessGroupProperties_FactorAccessGroupId",
                schema: "TAM",
                table: "FactorAccessGroupProperties",
                column: "FactorAccessGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FactorAccessGroupRoleOfOrganizations_FactorAccessGroupId",
                schema: "TAM",
                table: "FactorAccessGroupRoleOfOrganizations",
                column: "FactorAccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorAccessGroupRoleOfOrganizations_RoleOfOrganizationId",
                schema: "TAM",
                table: "FactorAccessGroupRoleOfOrganizations",
                column: "RoleOfOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorAccessGroupUsers_AccessGroupId",
                schema: "TAM",
                table: "FactorAccessGroupUsers",
                column: "AccessGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactorAccessGroupFactorType",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorAccessGroupGroups",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorAccessGroupOrganizationUnits",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorAccessGroupPermissions",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorAccessGroupProperties",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorAccessGroupRoleOfOrganizations",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FactorAccessGroupUsers",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "FactorAccessGroup",
                schema: "TAM");

            migrationBuilder.DropColumn(
                name: "DeliverablePen",
                schema: "TAM",
                table: "FactorServiceExplanations");

            migrationBuilder.DropColumn(
                name: "NettingProcessTypes",
                schema: "TAM",
                table: "FactorNettingProcessItems");
        }
    }
}
