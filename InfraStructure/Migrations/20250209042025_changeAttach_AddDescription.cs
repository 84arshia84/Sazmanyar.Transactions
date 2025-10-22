using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class changeAttach_AddDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attach_Contracts_ContractId",
                schema: "TAM",
                table: "Attach");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractGuarantees_ForGuarantee_ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractGuarantees_ReleaseCondition_ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees");

            migrationBuilder.DropIndex(
                name: "IX_Attach_ContractId",
                schema: "TAM",
                table: "Attach");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                schema: "TAM",
                table: "Attach",
                newName: "SectionId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "Description",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AuthorFullQualifyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WriteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Description", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ContractGuarantees_ForGuarantee_ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees",
                column: "ForGuaranteeId",
                principalSchema: "TAM",
                principalTable: "ForGuarantee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractGuarantees_ReleaseCondition_ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees",
                column: "ReleaseConditionId",
                principalSchema: "TAM",
                principalTable: "ReleaseCondition",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractGuarantees_ForGuarantee_ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractGuarantees_ReleaseCondition_ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees");

            migrationBuilder.DropTable(
                name: "Description",
                schema: "TAM");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                schema: "TAM",
                table: "Attach",
                newName: "ContractId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attach_ContractId",
                schema: "TAM",
                table: "Attach",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attach_Contracts_ContractId",
                schema: "TAM",
                table: "Attach",
                column: "ContractId",
                principalSchema: "TAM",
                principalTable: "Contracts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractGuarantees_ForGuarantee_ForGuaranteeId",
                schema: "TAM",
                table: "ContractGuarantees",
                column: "ForGuaranteeId",
                principalSchema: "TAM",
                principalTable: "ForGuarantee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractGuarantees_ReleaseCondition_ReleaseConditionId",
                schema: "TAM",
                table: "ContractGuarantees",
                column: "ReleaseConditionId",
                principalSchema: "TAM",
                principalTable: "ReleaseCondition",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
