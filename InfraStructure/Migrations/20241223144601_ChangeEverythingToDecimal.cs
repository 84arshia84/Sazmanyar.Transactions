using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEverythingToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedRequestedPercent",
                schema: "TAM",
                table: "ServiceExplanationFinancials");

            migrationBuilder.DropColumn(
                name: "ApprovedRequestedPrice",
                schema: "TAM",
                table: "ServiceExplanationFinancials");

            migrationBuilder.DropColumn(
                name: "ApprovedRequestedVolume",
                schema: "TAM",
                table: "ServiceExplanationFinancials");

            migrationBuilder.RenameTable(
                name: "DefaultCoefficients",
                schema: "TAM",
                newName: "DefaultCoefficients");

            migrationBuilder.AlterColumn<decimal>(
                name: "RequestedVolume",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "decimal(30,5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RequestedPrice",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "decimal(30,5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RequestedPercent",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "decimal(7,5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "ApprovedPercent",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "decimal(7,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ApprovedPrice",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "decimal(30,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ApprovedVolume",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "decimal(30,5)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitAmount",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProgramVolume",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrepaymentPercentage",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AccelerationRate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
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
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

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
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedPercent",
                schema: "TAM",
                table: "ServiceExplanationFinancials");

            migrationBuilder.DropColumn(
                name: "ApprovedPrice",
                schema: "TAM",
                table: "ServiceExplanationFinancials");

            migrationBuilder.DropColumn(
                name: "ApprovedVolume",
                schema: "TAM",
                table: "ServiceExplanationFinancials");

            migrationBuilder.RenameTable(
                name: "DefaultCoefficients",
                newName: "DefaultCoefficients",
                newSchema: "TAM");

            migrationBuilder.AlterColumn<string>(
                name: "RequestedVolume",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(decimal),
                oldType: "decimal(30,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestedPrice",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,5)");

            migrationBuilder.AlterColumn<string>(
                name: "RequestedPercent",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,5)");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedRequestedPercent",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedRequestedPrice",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedRequestedVolume",
                schema: "TAM",
                table: "ServiceExplanationFinancials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UnitAmount",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TotalAmount",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProgramVolume",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrepaymentPercentage",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "AccelerationRate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ContractCoefficients",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultRowValue",
                table: "ContractCoefficients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CoefficientValue",
                table: "ContractCoefficients",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "DefaultCoefficient",
                schema: "TAM",
                table: "DefaultCoefficients",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }
    }
}
