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
                    WordId = table.Column<int>(type: "int", nullable: false),
                    StatisticsId = table.Column<int>(type: "int", nullable: false)
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
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManCount = table.Column<int>(type: "int", nullable: false),
                    ManAvarageAge = table.Column<double>(type: "float", nullable: false),
                    WomanCount = table.Column<int>(type: "int", nullable: false),
                    WomanAvarageAge = table.Column<double>(type: "float", nullable: false),
                    AssociatedWordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statistics_AssociatedWord_AssociatedWordId",
                        column: x => x.AssociatedWordId,
                        principalTable: "AssociatedWord",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedWord_WordId",
                table: "AssociatedWord",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_AssociatedWordId",
                table: "Statistics",
                column: "AssociatedWordId",
                unique: true,
                filter: "[AssociatedWordId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "AssociatedWord");

            migrationBuilder.DropTable(
                name: "Word");
        }
    }
}
