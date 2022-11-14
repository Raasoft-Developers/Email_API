using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailService.Data.Migrations.SqlServerMigrations
{
    public partial class EmailErrorLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailErrorLogTable",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        principalTable: "EmailChannel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailErrorLogTable_EmailChannelID",
                table: "EmailErrorLogTable",
                column: "EmailChannelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailErrorLogTable");
        }
    }
}
