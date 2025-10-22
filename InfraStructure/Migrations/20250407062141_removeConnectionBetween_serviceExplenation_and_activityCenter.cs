using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class removeConnectionBetween_serviceExplenation_and_activityCenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupId",
            //    schema: "TAM",
            //    table: "InvoiceAccessGroupGroups");
            migrationBuilder.Sql(@"
                                 IF EXISTS (SELECT 1 FROM sys.foreign_keys 
                                     WHERE name = 'FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupId')
                                 BEGIN
                                    ALTER TABLE [TAM].[InvoiceAccessGroupGroups] DROP CONSTRAINT [FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupId];
                                 END
                                ");
            //migrationBuilder.DropForeignKey(
            //    name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupParentId",
            //    schema: "TAM",
            //    table: "InvoiceAccessGroupGroups");
            migrationBuilder.Sql(@"
                                 IF EXISTS (SELECT 1 FROM sys.foreign_keys 
                                     WHERE name = 'FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupParentId')
                                 BEGIN
                                    ALTER TABLE [TAM].[InvoiceAccessGroupGroups] DROP CONSTRAINT [FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupParentId];
                                 END
                                ");
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceExplanation_Activitycenters_ActivityCenterID",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.DropIndex(
                name: "IX_ServiceExplanation_ActivityCenterID",
                schema: "TAM",
                table: "ServiceExplanation");

            //migrationBuilder.DropIndex(
            //    name: "IX_InvoiceAccessGroupGroups_AccessGroupId",
            //    schema: "TAM",
            //    table: "InvoiceAccessGroupGroups");
            migrationBuilder.Sql(@"
                                 IF EXISTS (SELECT 1 FROM sys.indexes 
                                     WHERE name = 'IX_InvoiceAccessGroupGroups_AccessGroupId' AND object_id = OBJECT_ID('TAM.InvoiceAccessGroupGroups'))
                                 BEGIN
                                    DROP INDEX [IX_InvoiceAccessGroupGroups_AccessGroupId] ON [TAM].[InvoiceAccessGroupGroups];
                                 END
                                 ");

            //migrationBuilder.DropIndex(
            //    name: "IX_InvoiceAccessGroupGroups_AccessGroupParentId",
            //    schema: "TAM",
            //    table: "InvoiceAccessGroupGroups");

            migrationBuilder.Sql(@"
                                 IF EXISTS (SELECT 1 FROM sys.indexes 
                                     WHERE name = 'IX_InvoiceAccessGroupGroups_AccessGroupParentId' AND object_id = OBJECT_ID('TAM.InvoiceAccessGroupGroups'))
                                 BEGIN
                                    DROP INDEX [IX_InvoiceAccessGroupGroups_AccessGroupParentId] ON [TAM].[InvoiceAccessGroupGroups];
                                 END
                                 ");

            //migrationBuilder.DropColumn(
            //    name: "AccessGroupId",
            //    schema: "TAM",
            //    table: "InvoiceAccessGroupGroups");

            migrationBuilder.Sql(@"
    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_SCHEMA = 'TAM' 
              AND TABLE_NAME = 'InvoiceAccessGroupGroups' 
              AND COLUMN_NAME = 'AccessGroupId')
    BEGIN
        -- First drop constraints that might reference this column
        DECLARE @sql NVARCHAR(MAX) = '';
        
        -- Drop foreign key constraints
        SELECT @sql = @sql + 'ALTER TABLE [TAM].[InvoiceAccessGroupGroups] DROP CONSTRAINT ' + name + ';'
        FROM sys.foreign_keys
        WHERE parent_object_id = OBJECT_ID('TAM.InvoiceAccessGroupGroups')
        AND referenced_object_id = OBJECT_ID('TAM.InvoiceAccessGroupGroups');
        
        -- Drop default constraints
        SELECT @sql = @sql + 'ALTER TABLE [TAM].[InvoiceAccessGroupGroups] DROP CONSTRAINT ' + dc.name + ';'
        FROM sys.default_constraints dc
        JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
        WHERE c.object_id = OBJECT_ID('TAM.InvoiceAccessGroupGroups') AND c.name = 'AccessGroupId';
        
        EXEC sp_executesql @sql;
        
        -- Now drop the column
        ALTER TABLE [TAM].[InvoiceAccessGroupGroups] DROP COLUMN [AccessGroupId];
    END
");

            //migrationBuilder.DropColumn(
            //    name: "AccessGroupParentId",
            //    schema: "TAM",
            //    table: "InvoiceAccessGroupGroups");

            migrationBuilder.Sql(@"
    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_SCHEMA = 'TAM' 
              AND TABLE_NAME = 'InvoiceAccessGroupGroups' 
              AND COLUMN_NAME = 'AccessGroupParentId')
    BEGIN
        -- First drop constraints that might reference this column
        DECLARE @sql NVARCHAR(MAX) = '';
        
        -- Drop foreign key constraints
        SELECT @sql = @sql + 'ALTER TABLE [TAM].[InvoiceAccessGroupGroups] DROP CONSTRAINT ' + name + ';'
        FROM sys.foreign_keys
        WHERE parent_object_id = OBJECT_ID('TAM.InvoiceAccessGroupGroups')
        AND referenced_object_id = OBJECT_ID('TAM.InvoiceAccessGroupGroups');
        
        -- Drop default constraints
        SELECT @sql = @sql + 'ALTER TABLE [TAM].[InvoiceAccessGroupGroups] DROP CONSTRAINT ' + dc.name + ';'
        FROM sys.default_constraints dc
        JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
        WHERE c.object_id = OBJECT_ID('TAM.InvoiceAccessGroupGroups') AND c.name = 'AccessGroupParentId';
        
        EXEC sp_executesql @sql;
        
        -- Now drop the column
        ALTER TABLE [TAM].[InvoiceAccessGroupGroups] DROP COLUMN [AccessGroupParentId];
    END
");

            migrationBuilder.AddColumn<string>(
                name: "ActivityCenterTitle",
                schema: "TAM",
                table: "ServiceExplanation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "TAM",
                table: "InvoiceAccessGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_GroupId",
            //    schema: "TAM",
            //    table: "InvoiceAccessGroupGroups",
            //    column: "GroupId",
            //    principalSchema: "TAM",
            //    principalTable: "InvoiceAccessGroups",
            //    principalColumn: "Id");
            migrationBuilder.Sql(@"
    -- Check if both columns exist first
    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_SCHEMA = 'TAM' AND TABLE_NAME = 'InvoiceAccessGroupGroups' 
              AND COLUMN_NAME = 'GroupId')
    AND EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_SCHEMA = 'TAM' AND TABLE_NAME = 'InvoiceAccessGroups' 
               AND COLUMN_NAME = 'Id')
    BEGIN
        -- Check if constraint already exists
        IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys 
                      WHERE name = 'FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_GroupId')
        BEGIN
            -- Verify data integrity first
            IF NOT EXISTS (
                SELECT 1 FROM [TAM].[InvoiceAccessGroupGroups] g
                LEFT JOIN [TAM].[InvoiceAccessGroups] ag ON g.[GroupId] = ag.[Id]
                WHERE g.[GroupId] IS NOT NULL AND ag.[Id] IS NULL
            )
            BEGIN
                ALTER TABLE [TAM].[InvoiceAccessGroupGroups] 
                ADD CONSTRAINT [FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_GroupId] 
                FOREIGN KEY ([GroupId]) 
                REFERENCES [TAM].[InvoiceAccessGroups] ([Id]);
            END
            ELSE
            BEGIN
                PRINT 'Warning: Found orphaned GroupId references. Fix data before adding constraint.';
                -- Optional: Handle orphaned records here
                -- DELETE FROM [TAM].[InvoiceAccessGroupGroups] 
                -- WHERE GroupId IS NOT NULL 
                -- AND NOT EXISTS (SELECT 1 FROM [TAM].[InvoiceAccessGroups] WHERE Id = GroupId)
            END
        END
        ELSE
        BEGIN
            PRINT 'Constraint FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_GroupId already exists';
        END
    END
    ELSE
    BEGIN
        PRINT 'Required columns for foreign key constraint do not exist';
    END
");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_ParentGroupId",
            //    schema: "TAM",
            //    table: "InvoiceAccessGroupGroups",
            //    column: "ParentGroupId",
            //    principalSchema: "TAM",
            //    principalTable: "InvoiceAccessGroups",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql(@"
    -- Check if both columns exist first
    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_SCHEMA = 'TAM' AND TABLE_NAME = 'InvoiceAccessGroupGroups' 
              AND COLUMN_NAME = 'GroupId')
    AND EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_SCHEMA = 'TAM' AND TABLE_NAME = 'InvoiceAccessGroups' 
               AND COLUMN_NAME = 'Id')
    BEGIN
        -- Check if constraint already exists
        IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys 
                      WHERE name = 'FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_ParentGroupId')
        BEGIN
            -- Verify data integrity first
            IF NOT EXISTS (
                SELECT 1 FROM [TAM].[InvoiceAccessGroupGroups] g
                LEFT JOIN [TAM].[InvoiceAccessGroups] ag ON g.[GroupId] = ag.[Id]
                WHERE g.[GroupId] IS NOT NULL AND ag.[Id] IS NULL
            )
            BEGIN
                ALTER TABLE [TAM].[InvoiceAccessGroupGroups] 
                ADD CONSTRAINT [FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_ParentGroupId] 
                FOREIGN KEY ([GroupId]) 
                REFERENCES [TAM].[InvoiceAccessGroups] ([Id]);
            END
            ELSE
            BEGIN
                PRINT 'Warning: Found orphaned GroupId references. Fix data before adding constraint.';
                -- Optional: Handle orphaned records here
                -- DELETE FROM [TAM].[InvoiceAccessGroupGroups] 
                -- WHERE GroupId IS NOT NULL 
                -- AND NOT EXISTS (SELECT 1 FROM [TAM].[InvoiceAccessGroups] WHERE Id = GroupId)
            END
        END
        ELSE
        BEGIN
            PRINT 'Constraint FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_ParentGroupId already exists';
        END
    END
    ELSE
    BEGIN
        PRINT 'Required columns for foreign key constraint do not exist';
    END
");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_GroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_ParentGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups");

            migrationBuilder.DropColumn(
                name: "ActivityCenterTitle",
                schema: "TAM",
                table: "ServiceExplanation");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "TAM",
                table: "InvoiceAccessGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessGroupParentId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExplanation_ActivityCenterID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "ActivityCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupGroups_AccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "AccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceAccessGroupGroups_AccessGroupParentId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "AccessGroupParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "AccessGroupId",
                principalSchema: "TAM",
                principalTable: "InvoiceAccessGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceAccessGroupGroups_InvoiceAccessGroups_AccessGroupParentId",
                schema: "TAM",
                table: "InvoiceAccessGroupGroups",
                column: "AccessGroupParentId",
                principalSchema: "TAM",
                principalTable: "InvoiceAccessGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceExplanation_Activitycenters_ActivityCenterID",
                schema: "TAM",
                table: "ServiceExplanation",
                column: "ActivityCenterID",
                principalSchema: "TAM",
                principalTable: "Activitycenters",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
