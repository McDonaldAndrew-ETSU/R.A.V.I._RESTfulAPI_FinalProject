using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachineLearning_RESTfulAPI_FinalProject.Migrations
{
    public partial class Mig01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Information = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    CommandType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Action);
                });

            migrationBuilder.CreateTable(
                name: "Sentences",
                columns: table => new
                {
                    Spelling = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Meaning = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentences", x => x.Spelling);
                });

            migrationBuilder.CreateTable(
                name: "CommandSentences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandAction = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SentenceSpelling = table.Column<string>(type: "nvarchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandSentences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandSentences_Commands_CommandAction",
                        column: x => x.CommandAction,
                        principalTable: "Commands",
                        principalColumn: "Action",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandSentences_Sentences_SentenceSpelling",
                        column: x => x.SentenceSpelling,
                        principalTable: "Sentences",
                        principalColumn: "Spelling",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandSentences_CommandAction",
                table: "CommandSentences",
                column: "CommandAction");

            migrationBuilder.CreateIndex(
                name: "IX_CommandSentences_SentenceSpelling",
                table: "CommandSentences",
                column: "SentenceSpelling");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandSentences");

            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "Sentences");
        }
    }
}
