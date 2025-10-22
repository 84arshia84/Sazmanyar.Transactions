using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class remove_fromContractPermmision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Settings",
                schema: "TAM",
                table: "ContractAccessGroupPermissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Settings",
                schema: "TAM",
                table: "ContractAccessGroupPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
