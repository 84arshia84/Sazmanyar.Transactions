using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class changeTypeOfmoney : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UnitAmount",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "Decimal(30,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "Decimal(30,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProgramVolume",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "Decimal(30,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrepaymentPercentage",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "Decimal(7,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AccelerationRate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "Decimal(30,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageOfChanges",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(7,5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GoodJob_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(7,5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractValue_Added_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(7,5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractTax_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(7,5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractInsurance_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "Decimal(7,5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SumOfCoefficients",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "Decimal(10,5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RowPrice",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "Decimal(30,5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "Decimal(30,5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CoefficientValue",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "Decimal(10,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(30,5)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UnitAmount",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(30,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(30,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProgramVolume",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(30,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrepaymentPercentage",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(7,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AccelerationRate",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(30,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PercentageOfChanges",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(7,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GoodJob_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(7,5)");

            migrationBuilder.AlterColumn<string>(
                name: "ContractValue_Added_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(7,5)");

            migrationBuilder.AlterColumn<string>(
                name: "ContractTax_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(7,5)");

            migrationBuilder.AlterColumn<string>(
                name: "ContractInsurance_Percent",
                schema: "TAM",
                table: "ContractFinancialDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(7,5)");

            migrationBuilder.AlterColumn<string>(
                name: "SumOfCoefficients",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(10,5)");

            migrationBuilder.AlterColumn<string>(
                name: "RowPrice",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(30,5)");

            migrationBuilder.AlterColumn<string>(
                name: "Amount",
                schema: "TAM",
                table: "ContractEstimatedmeters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(30,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CoefficientValue",
                schema: "TAM",
                table: "ContractCoefficients",
                type: "Decimal(30,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(10,5)",
                oldNullable: true);
        }
    }
}
