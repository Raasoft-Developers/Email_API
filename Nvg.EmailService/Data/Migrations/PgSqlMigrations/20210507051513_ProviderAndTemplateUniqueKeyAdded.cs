using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.EmailService.data.Migrations.PgSqlMigrations
{
    public partial class ProviderAndTemplateUniqueKeyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_Name_EmailPoolID",
                schema: "Email",
                table: "EmailTemplate",
                columns: new[] { "Name", "EmailPoolID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailProviderSettings_Name_EmailPoolID",
                schema: "Email",
                table: "EmailProviderSettings",
                columns: new[] { "Name", "EmailPoolID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmailTemplate_Name_EmailPoolID",
                schema: "Email",
                table: "EmailTemplate");

            migrationBuilder.DropIndex(
                name: "IX_EmailProviderSettings_Name_EmailPoolID",
                schema: "Email",
                table: "EmailProviderSettings");
        }
    }
}
