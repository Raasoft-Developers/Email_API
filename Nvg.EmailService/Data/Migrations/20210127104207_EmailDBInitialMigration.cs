using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nvg.EmailService.Data.Migrations
{
    public partial class EmailDBInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Email");

            migrationBuilder.CreateTable(
                name: "EmailCounter",
                schema: "Email",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantID = table.Column<string>(nullable: true),
                    Count = table.Column<string>(nullable: true),
                    FacilityID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailCounter", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmailHistory",
                schema: "Email",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantID = table.Column<string>(nullable: true),
                    FacilityID = table.Column<string>(nullable: true),
                    ToEmailID = table.Column<string>(nullable: true),
                    MailBody = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    SentOn = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailHistory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                schema: "Email",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantID = table.Column<string>(nullable: true),
                    FacilityID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    EmailBodyTemplate = table.Column<string>(nullable: true),
                    SubjectTemplate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailCounter",
                schema: "Email");

            migrationBuilder.DropTable(
                name: "EmailHistory",
                schema: "Email");

            migrationBuilder.DropTable(
                name: "EmailTemplate",
                schema: "Email");
        }
    }
}
