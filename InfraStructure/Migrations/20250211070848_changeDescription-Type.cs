using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class changeDescriptionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Attach_Contracts_ContractId",
            //    schema: "TAM",
            //    table: "Attach");

            //migrationBuilder.DropIndex(
            //    name: "IX_Attach_ContractId",
            //    schema: "TAM",
            //    table: "Attach");

            //migrationBuilder.RenameColumn(
            //    name: "ContractId",
            //    schema: "TAM",
            //    table: "Attach",
            //    newName: "SectionId");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "GuaranteePrice",
            //    schema: "TAM",
            //    table: "ContractGuarantees",
            //    type: "Decimal(30,5)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");

            //migrationBuilder.CreateTable(
            //    name: "Description",
            //    schema: "TAM",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        AuthorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
            //        AuthorFullQualifyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
            //        WriteTime = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
            //        Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Description", x => x.Id);
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Description",
            //    schema: "TAM");

            //migrationBuilder.RenameColumn(
            //    name: "SectionId",
            //    schema: "TAM",
            //    table: "Attach",
            //    newName: "ContractId");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "GuaranteePrice",
            //    schema: "TAM",
            //    table: "ContractGuarantees",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "Decimal(30,5)");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Attach_ContractId",
            //    schema: "TAM",
            //    table: "Attach",
            //    column: "ContractId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Attach_Contracts_ContractId",
            //    schema: "TAM",
            //    table: "Attach",
            //    column: "ContractId",
            //    principalSchema: "TAM",
            //    principalTable: "Contracts",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
