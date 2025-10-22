using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_Guarantee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attach",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserUploader = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Section = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attach", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attach_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "TAM",
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfGuarantees",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfGuarantees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContractGuarantees",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuaranteePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValidityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RealeaseDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealeaseExplenation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForGuaranteeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOfGuaranteeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractGuarantees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractGuarantees_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "TAM",
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractGuarantees_ForGuarantee_ForGuaranteeId",
                        column: x => x.ForGuaranteeId,
                        principalSchema: "TAM",
                        principalTable: "ForGuarantee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractGuarantees_ReleaseCondition_ReleaseConditionId",
                        column: x => x.ReleaseConditionId,
                        principalSchema: "TAM",
                        principalTable: "ReleaseCondition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractGuarantees_TypeOfGuarantees_TypeOfGuaranteeId",
                        column: x => x.TypeOfGuaranteeId,
                        principalSchema: "TAM",
                        principalTable: "TypeOfGuarantees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attach_ContractId",
                schema: "TAM",
                table: "Attach",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractGuarantees_ContractId",
                schema: "TAM",
                table: "ContractGuarantees",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractGuarantees_ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees",
                column: "ForGuaranteeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractGuarantees_ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees",
                column: "ReleaseConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractGuarantees_TypeOfGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees",
                column: "TypeOfGuaranteeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attach",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ContractGuarantees",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "TypeOfGuarantees",
                schema: "TAM");
        }
    }
}
