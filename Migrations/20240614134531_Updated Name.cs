using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordNET_Server_2._0.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetAssociatedWordQuestionee_AssociatedWord_AssociatedWordId",
                table: "GetAssociatedWordQuestionee");

            migrationBuilder.DropForeignKey(
                name: "FK_GetAssociatedWordQuestionee_Questionee_QuestioneeId",
                table: "GetAssociatedWordQuestionee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetAssociatedWordQuestionee",
                table: "GetAssociatedWordQuestionee");

            migrationBuilder.RenameTable(
                name: "GetAssociatedWordQuestionee",
                newName: "AssociatedWordQuestionees");

            migrationBuilder.RenameIndex(
                name: "IX_GetAssociatedWordQuestionee_QuestioneeId",
                table: "AssociatedWordQuestionees",
                newName: "IX_AssociatedWordQuestionees_QuestioneeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssociatedWordQuestionees",
                table: "AssociatedWordQuestionees",
                columns: new[] { "AssociatedWordId", "QuestioneeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AssociatedWordQuestionees_AssociatedWord_AssociatedWordId",
                table: "AssociatedWordQuestionees",
                column: "AssociatedWordId",
                principalTable: "AssociatedWord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssociatedWordQuestionees_Questionee_QuestioneeId",
                table: "AssociatedWordQuestionees",
                column: "QuestioneeId",
                principalTable: "Questionee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssociatedWordQuestionees_AssociatedWord_AssociatedWordId",
                table: "AssociatedWordQuestionees");

            migrationBuilder.DropForeignKey(
                name: "FK_AssociatedWordQuestionees_Questionee_QuestioneeId",
                table: "AssociatedWordQuestionees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssociatedWordQuestionees",
                table: "AssociatedWordQuestionees");

            migrationBuilder.RenameTable(
                name: "AssociatedWordQuestionees",
                newName: "GetAssociatedWordQuestionee");

            migrationBuilder.RenameIndex(
                name: "IX_AssociatedWordQuestionees_QuestioneeId",
                table: "GetAssociatedWordQuestionee",
                newName: "IX_GetAssociatedWordQuestionee_QuestioneeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetAssociatedWordQuestionee",
                table: "GetAssociatedWordQuestionee",
                columns: new[] { "AssociatedWordId", "QuestioneeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GetAssociatedWordQuestionee_AssociatedWord_AssociatedWordId",
                table: "GetAssociatedWordQuestionee",
                column: "AssociatedWordId",
                principalTable: "AssociatedWord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GetAssociatedWordQuestionee_Questionee_QuestioneeId",
                table: "GetAssociatedWordQuestionee",
                column: "QuestioneeId",
                principalTable: "Questionee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
