using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.EmailService.data.Migrations.PgSqlMigrations
{
    public partial class ChannelKeyUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EmailChannel_Key",
                schema: "Email",
                table: "EmailChannel",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmailChannel_Key",
                schema: "Email",
                table: "EmailChannel");
        }
    }
}
