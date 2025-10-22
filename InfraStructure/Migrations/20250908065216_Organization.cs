using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class Organization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationInformation",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAcountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCodeAndName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ShabaNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvincId = table.Column<int>(type: "int", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    CountyId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationInformation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrganizationInformation_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "TAM",
                        principalTable: "City",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationInformation_County_CountyId",
                        column: x => x.CountyId,
                        principalSchema: "TAM",
                        principalTable: "County",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationInformation_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalSchema: "TAM",
                        principalTable: "Province",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Sheba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_OrganizationInformation_OrganizationInformationId",
                        column: x => x.OrganizationInformationId,
                        principalSchema: "TAM",
                        principalTable: "OrganizationInformation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OrganizationInformationId",
                schema: "TAM",
                table: "Accounts",
                column: "OrganizationInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationInformation_CityId",
                schema: "TAM",
                table: "OrganizationInformation",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationInformation_CountyId",
                schema: "TAM",
                table: "OrganizationInformation",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationInformation_ProvinceId",
                schema: "TAM",
                table: "OrganizationInformation",
                column: "ProvinceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "OrganizationInformation",
                schema: "TAM");
        }
    }
}
