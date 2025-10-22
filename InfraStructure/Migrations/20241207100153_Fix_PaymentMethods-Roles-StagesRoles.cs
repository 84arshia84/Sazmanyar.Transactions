using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_PaymentMethodsRolesStagesRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "StagesRoles",
                newName: "StagesRoles",
                newSchema: "TAM");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Roles",
                newSchema: "TAM");

            migrationBuilder.RenameTable(
                name: "PaymentMethods",
                newName: "PaymentMethods",
                newSchema: "TAM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "StagesRoles",
                schema: "TAM",
                newName: "StagesRoles");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "TAM",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "PaymentMethods",
                schema: "TAM",
                newName: "PaymentMethods");
        }
    }
}
