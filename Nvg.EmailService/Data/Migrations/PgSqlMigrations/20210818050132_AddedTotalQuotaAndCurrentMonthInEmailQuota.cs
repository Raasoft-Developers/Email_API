using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.EmailService.data.Migrations.PgSqlMigrations
{
    public partial class AddedTotalQuotaAndCurrentMonthInEmailQuota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentMonth",
                schema: "Email",
                table: "EmailQuota",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuota",
                schema: "Email",
                table: "EmailQuota",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentMonth",
                schema: "Email",
                table: "EmailQuota");

            migrationBuilder.DropColumn(
                name: "TotalQuota",
                schema: "Email",
                table: "EmailQuota");
        }
    }
}
