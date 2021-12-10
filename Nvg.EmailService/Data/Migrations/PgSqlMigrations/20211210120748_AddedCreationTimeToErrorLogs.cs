using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.EmailService.Data.Migrations.PgSqlMigrations
{
    public partial class AddedCreationTimeToErrorLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                schema: "Email",
                table: "EmailErrorLogTable",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "Email",
                table: "EmailErrorLogTable");
        }
    }
}
