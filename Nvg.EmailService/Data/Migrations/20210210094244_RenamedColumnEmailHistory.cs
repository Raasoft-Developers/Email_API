using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.EmailService.Data.Migrations
{
    public partial class RenamedColumnEmailHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MesssageSent",
                schema: "Email",
                table: "EmailHistory");

            migrationBuilder.AddColumn<string>(
                name: "MessageSent",
                schema: "Email",
                table: "EmailHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageSent",
                schema: "Email",
                table: "EmailHistory");

            migrationBuilder.AddColumn<string>(
                name: "MesssageSent",
                schema: "Email",
                table: "EmailHistory",
                type: "text",
                nullable: true);
        }
    }
}
