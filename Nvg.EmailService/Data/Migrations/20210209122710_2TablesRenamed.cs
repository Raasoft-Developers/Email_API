using Microsoft.EntityFrameworkCore.Migrations;

namespace Nvg.EmailService.Data.Migrations
{
    public partial class _2TablesRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailHistories_EmailChannel_EmailChannelID",
                schema: "Email",
                table: "EmailHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailHistories_EmailProviderSettings_EmailProviderID",
                schema: "Email",
                table: "EmailHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplates_EmailChannel_EmailChannelID",
                schema: "Email",
                table: "EmailTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTemplates",
                schema: "Email",
                table: "EmailTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailHistories",
                schema: "Email",
                table: "EmailHistories");

            migrationBuilder.RenameTable(
                name: "EmailTemplates",
                schema: "Email",
                newName: "EmailTemplate",
                newSchema: "Email");

            migrationBuilder.RenameTable(
                name: "EmailHistories",
                schema: "Email",
                newName: "EmailHistory",
                newSchema: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_EmailTemplates_EmailChannelID",
                schema: "Email",
                table: "EmailTemplate",
                newName: "IX_EmailTemplate_EmailChannelID");

            migrationBuilder.RenameIndex(
                name: "IX_EmailHistories_EmailProviderID",
                schema: "Email",
                table: "EmailHistory",
                newName: "IX_EmailHistory_EmailProviderID");

            migrationBuilder.RenameIndex(
                name: "IX_EmailHistories_EmailChannelID",
                schema: "Email",
                table: "EmailHistory",
                newName: "IX_EmailHistory_EmailChannelID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTemplate",
                schema: "Email",
                table: "EmailTemplate",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailHistory",
                schema: "Email",
                table: "EmailHistory",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailHistory_EmailChannel_EmailChannelID",
                schema: "Email",
                table: "EmailHistory",
                column: "EmailChannelID",
                principalSchema: "Email",
                principalTable: "EmailChannel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailHistory_EmailProviderSettings_EmailProviderID",
                schema: "Email",
                table: "EmailHistory",
                column: "EmailProviderID",
                principalSchema: "Email",
                principalTable: "EmailProviderSettings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailHistory_EmailChannel_EmailChannelID",
                schema: "Email",
                table: "EmailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailHistory_EmailProviderSettings_EmailProviderID",
                schema: "Email",
                table: "EmailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplate_EmailChannel_EmailChannelID",
                schema: "Email",
                table: "EmailTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTemplate",
                schema: "Email",
                table: "EmailTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailHistory",
                schema: "Email",
                table: "EmailHistory");

            migrationBuilder.RenameTable(
                name: "EmailTemplate",
                schema: "Email",
                newName: "EmailTemplates",
                newSchema: "Email");

            migrationBuilder.RenameTable(
                name: "EmailHistory",
                schema: "Email",
                newName: "EmailHistories",
                newSchema: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_EmailTemplate_EmailChannelID",
                schema: "Email",
                table: "EmailTemplates",
                newName: "IX_EmailTemplates_EmailChannelID");

            migrationBuilder.RenameIndex(
                name: "IX_EmailHistory_EmailProviderID",
                schema: "Email",
                table: "EmailHistories",
                newName: "IX_EmailHistories_EmailProviderID");

            migrationBuilder.RenameIndex(
                name: "IX_EmailHistory_EmailChannelID",
                schema: "Email",
                table: "EmailHistories",
                newName: "IX_EmailHistories_EmailChannelID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTemplates",
                schema: "Email",
                table: "EmailTemplates",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailHistories",
                schema: "Email",
                table: "EmailHistories",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailHistories_EmailChannel_EmailChannelID",
                schema: "Email",
                table: "EmailHistories",
                column: "EmailChannelID",
                principalSchema: "Email",
                principalTable: "EmailChannel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailHistories_EmailProviderSettings_EmailProviderID",
                schema: "Email",
                table: "EmailHistories",
                column: "EmailProviderID",
                principalSchema: "Email",
                principalTable: "EmailProviderSettings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplates_EmailChannel_EmailChannelID",
                schema: "Email",
                table: "EmailTemplates",
                column: "EmailChannelID",
                principalSchema: "Email",
                principalTable: "EmailChannel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
