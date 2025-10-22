using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class add_onAccountMode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OnAccountDepreciation",
                schema: "TAM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceExplenationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceExplenationFinancialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DepreciationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuggestedDepreciationAmount = table.Column<decimal>(type: "decimal(30,5)", nullable: false, defaultValue: 0m),
                    ApprovedDepreciationAmount = table.Column<decimal>(type: "decimal(30,5)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnAccountDepreciation", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnAccountDepreciation",
                schema: "TAM");
        }
    }
}
