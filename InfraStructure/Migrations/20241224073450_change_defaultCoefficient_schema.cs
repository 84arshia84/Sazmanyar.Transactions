using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class change_defaultCoefficient_schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "DefaultCoefficients",
                newName: "DefaultCoefficients",
                newSchema: "TAM");

            migrationBuilder.RenameTable(
                name: "ContractCoefficients",
                newName: "ContractCoefficients",
                newSchema: "TAM");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "TAM",
                table: "DefaultCoefficients",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultRows",
                schema: "TAM",
                table: "DefaultCoefficients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultCoefficient",
                schema: "TAM",
                table: "DefaultCoefficients",
                type: "Decimal(30,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultRowValue",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CoefficientValue",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "Decimal(30,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "DefaultCoefficients",
                schema: "TAM",
                newName: "DefaultCoefficients");

            migrationBuilder.RenameTable(
                name: "ContractCoefficients",
                schema: "TAM",
                newName: "ContractCoefficients");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "DefaultCoefficients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "DefaultRows",
                table: "DefaultCoefficients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultCoefficient",
                table: "DefaultCoefficients",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(30,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ContractCoefficients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "DefaultRowValue",
                table: "ContractCoefficients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CoefficientValue",
                table: "ContractCoefficients",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(30,5)",
                oldNullable: true);
        }
    }
}
