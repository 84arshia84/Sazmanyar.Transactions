using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class chage_organUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAcountNumber",
                schema: "TAM",
                table: "OrganizationInformation");

            migrationBuilder.DropColumn(
                name: "BankName",
                schema: "TAM",
                table: "OrganizationInformation");

            migrationBuilder.DropColumn(
                name: "BranchCodeAndName",
                schema: "TAM",
                table: "OrganizationInformation");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "TAM",
                table: "OrganizationInformation");

            migrationBuilder.DropColumn(
                name: "ShabaNumber",
                schema: "TAM",
                table: "OrganizationInformation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAcountNumber",
                schema: "TAM",
                table: "OrganizationInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                schema: "TAM",
                table: "OrganizationInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchCodeAndName",
                schema: "TAM",
                table: "OrganizationInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "TAM",
                table: "OrganizationInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ShabaNumber",
                schema: "TAM",
                table: "OrganizationInformation",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
