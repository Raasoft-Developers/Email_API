using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nvg.EmailService.Data.Migrations.PgSqlMigrations
{
    public partial class EmailErrorLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailErrorLogTable",
                schema: "Email",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ErrorType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true),
                    Recipients = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    EmailChannelID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailErrorLogTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmailErrorLogTable_EmailChannel_EmailChannelID",
                        column: x => x.EmailChannelID,
                        principalSchema: "Email",
                        principalTable: "EmailChannel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailErrorLogTable_EmailChannelID",
                schema: "Email",
                table: "EmailErrorLogTable",
                column: "EmailChannelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailErrorLogTable",
                schema: "Email");
        }
    }
}
