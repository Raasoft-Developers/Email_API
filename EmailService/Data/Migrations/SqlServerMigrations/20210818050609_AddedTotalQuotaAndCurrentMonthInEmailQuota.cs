using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailService.data.Migrations.SqlServerMigrations
{
    public partial class AddedTotalQuotaAndCurrentMonthInEmailQuota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EmailTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentMonth",
                table: "EmailQuota",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuota",
                table: "EmailQuota",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EmailProviderSettings",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "EmailChannel",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_Name_EmailPoolID",
                table: "EmailTemplate",
                columns: new[] { "Name", "EmailPoolID" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [EmailPoolID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailProviderSettings_Name_EmailPoolID",
                table: "EmailProviderSettings",
                columns: new[] { "Name", "EmailPoolID" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [EmailPoolID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailChannel_Key",
                table: "EmailChannel",
                column: "Key",
                unique: true,
                filter: "[Key] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmailTemplate_Name_EmailPoolID",
                table: "EmailTemplate");

            migrationBuilder.DropIndex(
                name: "IX_EmailProviderSettings_Name_EmailPoolID",
                table: "EmailProviderSettings");

            migrationBuilder.DropIndex(
                name: "IX_EmailChannel_Key",
                table: "EmailChannel");

            migrationBuilder.DropColumn(
                name: "CurrentMonth",
                table: "EmailQuota");

            migrationBuilder.DropColumn(
                name: "TotalQuota",
                table: "EmailQuota");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EmailTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EmailProviderSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "EmailChannel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
