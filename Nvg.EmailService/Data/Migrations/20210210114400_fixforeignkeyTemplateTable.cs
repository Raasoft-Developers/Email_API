using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.EmailService.Data.Migrations
{
    public partial class fixforeignkeyTemplateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplate_EmailChannel_EmailChannelID",
                schema: "Email",
                table: "EmailTemplate");

            migrationBuilder.DropIndex(
                name: "IX_EmailTemplate_EmailChannelID",
                schema: "Email",
                table: "EmailTemplate");

            migrationBuilder.DropColumn(
                name: "EmailChannelID",
                schema: "Email",
                table: "EmailTemplate");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_EmailPoolID",
                schema: "Email",
                table: "EmailTemplate",
                column: "EmailPoolID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplate_EmailChannel_EmailPoolID",
                schema: "Email",
                table: "EmailTemplate");

            migrationBuilder.DropIndex(
                name: "IX_EmailTemplate_EmailPoolID",
                schema: "Email",
                table: "EmailTemplate");

            migrationBuilder.AddColumn<string>(
                name: "EmailChannelID",
                schema: "Email",
                table: "EmailTemplate",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_EmailChannelID",
                schema: "Email",
                table: "EmailTemplate",
                column: "EmailChannelID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplate_EmailChannel_EmailChannelID",
                schema: "Email",
                table: "EmailTemplate",
                column: "EmailChannelID",
                principalSchema: "Email",
                principalTable: "EmailChannel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
