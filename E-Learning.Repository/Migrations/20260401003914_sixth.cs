using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.Repository.Migrations
{
    /// <inheritdoc />
    public partial class sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuizAttemptAnswers_AttemptId",
                table: "QuizAttemptAnswers");

            migrationBuilder.AddColumn<int>(
                name: "QuizAttemptAnswerId",
                table: "QuizOptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NeedsReview",
                table: "QuizAttemptAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_QuizOptions_QuizAttemptAnswerId",
                table: "QuizOptions",
                column: "QuizAttemptAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttemptAnswers_AttemptId_QuestionId",
                table: "QuizAttemptAnswers",
                columns: new[] { "AttemptId", "QuestionId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizOptions_QuizAttemptAnswers_QuizAttemptAnswerId",
                table: "QuizOptions",
                column: "QuizAttemptAnswerId",
                principalTable: "QuizAttemptAnswers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizOptions_QuizAttemptAnswers_QuizAttemptAnswerId",
                table: "QuizOptions");

            migrationBuilder.DropIndex(
                name: "IX_QuizOptions_QuizAttemptAnswerId",
                table: "QuizOptions");

            migrationBuilder.DropIndex(
                name: "IX_QuizAttemptAnswers_AttemptId_QuestionId",
                table: "QuizAttemptAnswers");

            migrationBuilder.DropColumn(
                name: "QuizAttemptAnswerId",
                table: "QuizOptions");

            migrationBuilder.DropColumn(
                name: "NeedsReview",
                table: "QuizAttemptAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttemptAnswers_AttemptId",
                table: "QuizAttemptAnswers",
                column: "AttemptId");
        }
    }
}
