using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class changeType_decimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        IF EXISTS (SELECT 1 FROM sys.foreign_keys 
                  WHERE name = 'FK_Attach_Contracts_ContractId')
        BEGIN
            ALTER TABLE [TAM].[Attach] DROP CONSTRAINT [FK_Attach_Contracts_ContractId];
        END
    ");

            migrationBuilder.Sql(@"
    IF EXISTS (SELECT 1 FROM sys.indexes 
              WHERE name = 'IX_Attach_ContractId' AND object_id = OBJECT_ID('TAM.Attach'))
    BEGIN
        DROP INDEX [IX_Attach_ContractId] ON [TAM].[Attach];
    END
");


            migrationBuilder.Sql(@"
    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_SCHEMA = 'TAM' 
              AND TABLE_NAME = 'Attach' 
              AND COLUMN_NAME = 'ContractId')
    BEGIN
        -- First drop constraints that might reference this column
        DECLARE @sql NVARCHAR(MAX) = '';
        
        -- Drop foreign key constraints
        SELECT @sql = @sql + 'ALTER TABLE [TAM].[Attach] DROP CONSTRAINT ' + name + ';'
        FROM sys.foreign_keys
        WHERE parent_object_id = OBJECT_ID('TAM.Attach')
        AND referenced_object_id = OBJECT_ID('TAM.Contracts');
        
        -- Drop default constraints
        SELECT @sql = @sql + 'ALTER TABLE [TAM].[Attach] DROP CONSTRAINT ' + dc.name + ';'
        FROM sys.default_constraints dc
        JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
        WHERE c.object_id = OBJECT_ID('TAM.Attach') AND c.name = 'ContractId';
        
        EXEC sp_executesql @sql;
        
        -- Now drop the column
        ALTER TABLE [TAM].[Attach] DROP COLUMN [ContractId];
    END
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContractId",
                schema: "TAM",
                table: "Attach",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
        }
    }
}
