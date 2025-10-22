using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class addLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                schema: "TAM",
                table: "CorespondentReal",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CertificateNumber",
                schema: "TAM",
                table: "CorespondentReal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                schema: "TAM",
                table: "CorespondentReal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountyId",
                schema: "TAM",
                table: "CorespondentReal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EconomicCode",
                schema: "TAM",
                table: "CorespondentReal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "TAM",
                table: "CorespondentReal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                schema: "TAM",
                table: "CorespondentReal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "TAM",
                table: "CorespondentReal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                schema: "TAM",
                table: "CorespondentReal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvincId",
                schema: "TAM",
                table: "CorespondentReal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                schema: "TAM",
                table: "CorespondentLegal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountyId",
                schema: "TAM",
                table: "CorespondentLegal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EconomicCode",
                schema: "TAM",
                table: "CorespondentLegal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "TAM",
                table: "CorespondentLegal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "TAM",
                table: "CorespondentLegal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                schema: "TAM",
                table: "CorespondentLegal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvincId",
                schema: "TAM",
                table: "CorespondentLegal",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Province",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "County",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_County", x => x.Id);
                    table.ForeignKey(
                        name: "FK_County_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalSchema: "TAM",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_County_CountyId",
                        column: x => x.CountyId,
                        principalSchema: "TAM",
                        principalTable: "County",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorespondentReal_CityId",
                schema: "TAM",
                table: "CorespondentReal",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CorespondentReal_CountyId",
                schema: "TAM",
                table: "CorespondentReal",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_CorespondentReal_ProvincId",
                schema: "TAM",
                table: "CorespondentReal",
                column: "ProvincId");

            migrationBuilder.CreateIndex(
                name: "IX_CorespondentLegal_CityId",
                schema: "TAM",
                table: "CorespondentLegal",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CorespondentLegal_CountyId",
                schema: "TAM",
                table: "CorespondentLegal",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_CorespondentLegal_ProvincId",
                schema: "TAM",
                table: "CorespondentLegal",
                column: "ProvincId");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountyId",
                schema: "TAM",
                table: "City",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_County_ProvinceId",
                schema: "TAM",
                table: "County",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorespondentLegal_City_CityId",
                schema: "TAM",
                table: "CorespondentLegal",
                column: "CityId",
                principalSchema: "TAM",
                principalTable: "City",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorespondentLegal_County_CountyId",
                schema: "TAM",
                table: "CorespondentLegal",
                column: "CountyId",
                principalSchema: "TAM",
                principalTable: "County",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorespondentLegal_Province_ProvincId",
                schema: "TAM",
                table: "CorespondentLegal",
                column: "ProvincId",
                principalSchema: "TAM",
                principalTable: "Province",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorespondentReal_City_CityId",
                schema: "TAM",
                table: "CorespondentReal",
                column: "CityId",
                principalSchema: "TAM",
                principalTable: "City",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorespondentReal_County_CountyId",
                schema: "TAM",
                table: "CorespondentReal",
                column: "CountyId",
                principalSchema: "TAM",
                principalTable: "County",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorespondentReal_Province_ProvincId",
                schema: "TAM",
                table: "CorespondentReal",
                column: "ProvincId",
                principalSchema: "TAM",
                principalTable: "Province",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorespondentLegal_City_CityId",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.DropForeignKey(
                name: "FK_CorespondentLegal_County_CountyId",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.DropForeignKey(
                name: "FK_CorespondentLegal_Province_ProvincId",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.DropForeignKey(
                name: "FK_CorespondentReal_City_CityId",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropForeignKey(
                name: "FK_CorespondentReal_County_CountyId",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropForeignKey(
                name: "FK_CorespondentReal_Province_ProvincId",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropTable(
                name: "City",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "County",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "Province",
                schema: "TAM");

            migrationBuilder.DropIndex(
                name: "IX_CorespondentReal_CityId",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropIndex(
                name: "IX_CorespondentReal_CountyId",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropIndex(
                name: "IX_CorespondentReal_ProvincId",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropIndex(
                name: "IX_CorespondentLegal_CityId",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.DropIndex(
                name: "IX_CorespondentLegal_CountyId",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.DropIndex(
                name: "IX_CorespondentLegal_ProvincId",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.DropColumn(
                name: "CertificateNumber",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropColumn(
                name: "CountyId",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropColumn(
                name: "EconomicCode",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropColumn(
                name: "FatherName",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropColumn(
                name: "ProvincId",
                schema: "TAM",
                table: "CorespondentReal");

            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.DropColumn(
                name: "CountyId",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.DropColumn(
                name: "EconomicCode",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.DropColumn(
                name: "ProvincId",
                schema: "TAM",
                table: "CorespondentLegal");

            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                schema: "TAM",
                table: "CorespondentReal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
