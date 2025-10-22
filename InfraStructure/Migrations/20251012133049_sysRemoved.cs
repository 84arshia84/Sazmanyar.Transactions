using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    public partial class sysRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // چون ستون SystemParts وجود نداره، کاری انجام نمی‌دیم
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SystemParts",
                schema: "TAM",
                table: "CheckLists",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
