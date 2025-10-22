using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class _14030724 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TAM");

            migrationBuilder.CreateTable(
                name: "Activitycenters",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activitycenters", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BasisForStartingTheProject",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasisForStartingTheProject", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BasisFortheEndOftheProject",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasisFortheEndOftheProject", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CorespondentLegal",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAcountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCodeAndName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsReal = table.Column<bool>(type: "bit", nullable: false),
                    ShabaNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorespondentLegal", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CorespondentReal",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAcountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCodeAndName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsReal = table.Column<bool>(type: "bit", nullable: false),
                    ShabaNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Family = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorespondentReal", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CreditSource",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreditSourceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditSource", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExtensionType",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtensionType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FinePaymentMethod",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinePaymentMethod", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ForGuarantee",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForGuarantee", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HowToPay",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HowToPay", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Monetaryunit",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monetaryunit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Organizationalunit",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizationalunit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReasonForCancellation",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonForCancellation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReasonForTermination",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonForTermination", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseCondition",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseCondition", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TransActionTypes",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransActionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfCooperation",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfCooperation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CorespondAndTypeOfCoopRel",
                schema: "TAM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoresponedLegalID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CorespondentLegalID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CoresponedRealID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CorespondentRealID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TypeOfCoopreationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TypeOfCooperationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorespondAndTypeOfCoopRel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CorespondAndTypeOfCoopRel_CorespondentLegal_CorespondentLegalID",
                        column: x => x.CorespondentLegalID,
                        principalSchema: "TAM",
                        principalTable: "CorespondentLegal",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CorespondAndTypeOfCoopRel_CorespondentLegal_CoresponedLegalID",
                        column: x => x.CoresponedLegalID,
                        principalSchema: "TAM",
                        principalTable: "CorespondentLegal",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CorespondAndTypeOfCoopRel_CorespondentReal_CorespondentRealID",
                        column: x => x.CorespondentRealID,
                        principalSchema: "TAM",
                        principalTable: "CorespondentReal",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CorespondAndTypeOfCoopRel_CorespondentReal_CoresponedRealID",
                        column: x => x.CoresponedRealID,
                        principalSchema: "TAM",
                        principalTable: "CorespondentReal",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CorespondAndTypeOfCoopRel_TypeOfCooperation_TypeOfCooperationID",
                        column: x => x.TypeOfCooperationID,
                        principalSchema: "TAM",
                        principalTable: "TypeOfCooperation",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CorespondAndTypeOfCoopRel_TypeOfCooperation_TypeOfCoopreationID",
                        column: x => x.TypeOfCoopreationID,
                        principalSchema: "TAM",
                        principalTable: "TypeOfCooperation",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorespondAndTypeOfCoopRel_CorespondentLegalID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "CorespondentLegalID");

            migrationBuilder.CreateIndex(
                name: "IX_CorespondAndTypeOfCoopRel_CorespondentRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "CorespondentRealID");

            migrationBuilder.CreateIndex(
                name: "IX_CorespondAndTypeOfCoopRel_CoresponedLegalID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "CoresponedLegalID");

            migrationBuilder.CreateIndex(
                name: "IX_CorespondAndTypeOfCoopRel_CoresponedRealID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "CoresponedRealID");

            migrationBuilder.CreateIndex(
                name: "IX_CorespondAndTypeOfCoopRel_TypeOfCooperationID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "TypeOfCooperationID");

            migrationBuilder.CreateIndex(
                name: "IX_CorespondAndTypeOfCoopRel_TypeOfCoopreationID",
                schema: "TAM",
                table: "CorespondAndTypeOfCoopRel",
                column: "TypeOfCoopreationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activitycenters",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "BasisForStartingTheProject",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "BasisFortheEndOftheProject",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "CorespondAndTypeOfCoopRel",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "CreditSource",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ExtensionType",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "FinePaymentMethod",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ForGuarantee",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "HowToPay",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "Monetaryunit",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "Organizationalunit",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ReasonForCancellation",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ReasonForTermination",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "ReleaseCondition",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "TransActionTypes",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "CorespondentLegal",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "CorespondentReal",
                schema: "TAM");

            migrationBuilder.DropTable(
                name: "TypeOfCooperation",
                schema: "TAM");
        }
    }
}
