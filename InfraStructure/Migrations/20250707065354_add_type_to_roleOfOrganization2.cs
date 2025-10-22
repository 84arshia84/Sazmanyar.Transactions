using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_type_to_roleOfOrganization2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrganizationRoleTypeEnum",
                schema: "TAM",
                table: "RoleOfOrganizations",
                newName: "OrganizationRoleType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrganizationRoleType",
                schema: "TAM",
                table: "RoleOfOrganizations",
                newName: "OrganizationRoleTypeEnum");
        }
    }
}
