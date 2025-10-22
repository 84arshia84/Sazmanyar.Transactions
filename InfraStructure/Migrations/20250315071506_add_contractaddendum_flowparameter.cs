using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_contractaddendum_flowparameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentStageId",
                schema: "TAM",
                table: "ContractAddendums",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CurrentStatusTitle",
                schema: "TAM",
                table: "ContractAddendums",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalApprove",
                schema: "TAM",
                table: "ContractAddendums",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastActionTitle",
                schema: "TAM",
                table: "ContractAddendums",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStageId",
                schema: "TAM",
                table: "ContractAddendums");

            migrationBuilder.DropColumn(
                name: "CurrentStatusTitle",
                schema: "TAM",
                table: "ContractAddendums");

            migrationBuilder.DropColumn(
                name: "IsFinalApprove",
                schema: "TAM",
                table: "ContractAddendums");

            migrationBuilder.DropColumn(
                name: "LastActionTitle",
                schema: "TAM",
                table: "ContractAddendums");
        }
    }
}
