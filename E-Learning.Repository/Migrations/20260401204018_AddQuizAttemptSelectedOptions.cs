using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizAttemptSelectedOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizOptions_QuizAttemptAnswers_QuizAttemptAnswerId",
                table: "QuizOptions");

            migrationBuilder.DropIndex(
                name: "IX_QuizOptions_QuizAttemptAnswerId",
                table: "QuizOptions");

            migrationBuilder.DropColumn(
                name: "QuizAttemptAnswerId",
                table: "QuizOptions");

            migrationBuilder.CreateTable(
                name: "QuizAttemptAnswerSelectedOptions",
                columns: table => new
                {
                    QuizAttemptAnswerId = table.Column<int>(type: "int", nullable: false),
                    SelectedOptionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttemptAnswerSelectedOptions", x => new { x.QuizAttemptAnswerId, x.SelectedOptionsId });
                    table.ForeignKey(
                        name: "FK_QuizAttemptAnswerSelectedOptions_QuizAttemptAnswers_QuizAttemptAnswerId",
                        column: x => x.QuizAttemptAnswerId,
                        principalTable: "QuizAttemptAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizAttemptAnswerSelectedOptions_QuizOptions_SelectedOptionsId",
                        column: x => x.SelectedOptionsId,
                        principalTable: "QuizOptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttemptAnswerSelectedOptions_SelectedOptionsId",
                table: "QuizAttemptAnswerSelectedOptions",
                column: "SelectedOptionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizAttemptAnswerSelectedOptions");

            migrationBuilder.AddColumn<int>(
                name: "QuizAttemptAnswerId",
                table: "QuizOptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizOptions_QuizAttemptAnswerId",
                table: "QuizOptions",
                column: "QuizAttemptAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizOptions_QuizAttemptAnswers_QuizAttemptAnswerId",
                table: "QuizOptions",
                column: "QuizAttemptAnswerId",
                principalTable: "QuizAttemptAnswers",
                principalColumn: "Id");
        }
    }
}
