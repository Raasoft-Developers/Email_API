using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.EmailService.Data.Migrations
{
    public partial class FixTemplateTableProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplate_EmailChannel_EmailPoolID",
                schema: "Email",
                table: "EmailTemplate");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplate_EmailPool_EmailPoolID",
                schema: "Email",
                table: "EmailTemplate",
                column: "EmailPoolID",
                principalSchema: "Email",
                principalTable: "EmailPool",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplate_EmailPool_EmailPoolID",
                schema: "Email",
                table: "EmailTemplate");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplate_EmailChannel_EmailPoolID",
                schema: "Email",
                table: "EmailTemplate",
                column: "EmailPoolID",
                principalSchema: "Email",
                principalTable: "EmailChannel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
