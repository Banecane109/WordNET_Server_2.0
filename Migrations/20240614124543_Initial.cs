using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordNET_Server_2._0.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questionee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsMan = table.Column<bool>(type: "bit", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssociatedWord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    WordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociatedWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociatedWord_Word_WordId",
                        column: x => x.WordId,
                        principalTable: "Word",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GetAssociatedWordQuestionee",
                columns: table => new
                {
                    AssociatedWordId = table.Column<int>(type: "int", nullable: false),
                    QuestioneeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetAssociatedWordQuestionee", x => new { x.AssociatedWordId, x.QuestioneeId });
                    table.ForeignKey(
                        name: "FK_GetAssociatedWordQuestionee_AssociatedWord_AssociatedWordId",
                        column: x => x.AssociatedWordId,
                        principalTable: "AssociatedWord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GetAssociatedWordQuestionee_Questionee_QuestioneeId",
                        column: x => x.QuestioneeId,
                        principalTable: "Questionee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedWord_WordId",
                table: "AssociatedWord",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_GetAssociatedWordQuestionee_QuestioneeId",
                table: "GetAssociatedWordQuestionee",
                column: "QuestioneeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetAssociatedWordQuestionee");

            migrationBuilder.DropTable(
                name: "AssociatedWord");

            migrationBuilder.DropTable(
                name: "Questionee");

            migrationBuilder.DropTable(
                name: "Word");
        }
    }
}
