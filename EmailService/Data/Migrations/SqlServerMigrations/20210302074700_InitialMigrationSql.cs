using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EmailService.data.Migrations.SqlServerMigrations
{
    public partial class InitialMigrationSql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailPool",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailPool", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmailProviderSettings",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Configuration = table.Column<string>(nullable: true),
                    EmailPoolID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailProviderSettings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmailProviderSettings_EmailPool_EmailPoolID",
                        column: x => x.EmailPoolID,
                        principalTable: "EmailPool",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Variant = table.Column<string>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    EmailPoolID = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmailTemplate_EmailPool_EmailPoolID",
                        column: x => x.EmailPoolID,
                        principalTable: "EmailPool",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailChannel",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    EmailPoolID = table.Column<string>(nullable: true),
                    EmailProviderID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailChannel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmailChannel_EmailPool_EmailPoolID",
                        column: x => x.EmailPoolID,
                        principalTable: "EmailPool",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailChannel_EmailProviderSettings_EmailProviderID",
                        column: x => x.EmailProviderID,
                        principalTable: "EmailProviderSettings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailHistory",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    MessageSent = table.Column<string>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    Recipients = table.Column<string>(nullable: true),
                    SentOn = table.Column<DateTime>(nullable: false),
                    TemplateName = table.Column<string>(nullable: true),
                    TemplateVariant = table.Column<string>(nullable: true),
                    EmailChannelID = table.Column<string>(nullable: true),
                    EmailProviderID = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Attempts = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmailHistory_EmailChannel_EmailChannelID",
                        column: x => x.EmailChannelID,
                        principalTable: "EmailChannel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailHistory_EmailProviderSettings_EmailProviderID",
                        column: x => x.EmailProviderID,
                        principalTable: "EmailProviderSettings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailQuota",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailChannelID = table.Column<string>(nullable: true),
                    TotalConsumption = table.Column<int>(nullable: false),
                    MonthlyConsumption = table.Column<int>(nullable: false),
                    MonthlyQuota = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailQuota", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmailQuota_EmailChannel_EmailChannelID",
                        column: x => x.EmailChannelID,
                        principalTable: "EmailChannel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailChannel_EmailPoolID",
                table: "EmailChannel",
                column: "EmailPoolID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailChannel_EmailProviderID",
                table: "EmailChannel",
                column: "EmailProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailHistory_EmailChannelID",
                table: "EmailHistory",
                column: "EmailChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailHistory_EmailProviderID",
                table: "EmailHistory",
                column: "EmailProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailPool_Name",
                table: "EmailPool",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailProviderSettings_EmailPoolID",
                table: "EmailProviderSettings",
                column: "EmailPoolID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailQuota_EmailChannelID",
                table: "EmailQuota",
                column: "EmailChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_EmailPoolID",
                table: "EmailTemplate",
                column: "EmailPoolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailHistory");

            migrationBuilder.DropTable(
                name: "EmailQuota");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "EmailChannel");

            migrationBuilder.DropTable(
                name: "EmailProviderSettings");

            migrationBuilder.DropTable(
                name: "EmailPool");
        }
    }
}
