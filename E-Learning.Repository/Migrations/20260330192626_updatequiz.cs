using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updatequiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaxAttempts",
                table: "Quizzes",
                type: "int",
                nullable: true,
                defaultValue: 3,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 3);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "QuizAttempts",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "QuizAttempts");

            migrationBuilder.AlterColumn<int>(
                name: "MaxAttempts",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 3,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 3);
        }
    }
}
