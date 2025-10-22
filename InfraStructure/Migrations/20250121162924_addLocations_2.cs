using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class addLocations_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                schema: "TAM",
                table: "City",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                schema: "TAM",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Province_ProvinceId",
                schema: "TAM",
                table: "City",
                column: "ProvinceId",
                principalSchema: "TAM",
                principalTable: "Province",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Province_ProvinceId",
                schema: "TAM",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_City_ProvinceId",
                schema: "TAM",
                table: "City");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                schema: "TAM",
                table: "City");
        }
    }
}
