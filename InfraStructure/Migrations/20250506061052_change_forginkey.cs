using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class change_forginkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionExecutionRequests_Organizationalunit_OrganizationalunitID",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropIndex(
                name: "IX_TransactionExecutionRequests_OrganizationalunitID",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropColumn(
                name: "OrganizationalunitID",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeleteBy",
                schema: "TAM",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeleteBy",
                schema: "TAM",
                table: "NettingProcessItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionExecutionRequests_OrganizationId",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionExecutionRequests_Organizationalunit_OrganizationId",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                column: "OrganizationId",
                principalSchema: "TAM",
                principalTable: "Organizationalunit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionExecutionRequests_Organizationalunit_OrganizationId",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.DropIndex(
                name: "IX_TransactionExecutionRequests_OrganizationId",
                schema: "TAM",
                table: "TransactionExecutionRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationalunitID",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DeleteBy",
                schema: "TAM",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DeleteBy",
                schema: "TAM",
                table: "NettingProcessItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionExecutionRequests_OrganizationalunitID",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                column: "OrganizationalunitID");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionExecutionRequests_Organizationalunit_OrganizationalunitID",
                schema: "TAM",
                table: "TransactionExecutionRequests",
                column: "OrganizationalunitID",
                principalSchema: "TAM",
                principalTable: "Organizationalunit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
