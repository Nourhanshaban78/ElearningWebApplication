using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.Repository.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndAt",
                table: "Quizzes",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndAt",
                table: "Quizzes");
        }
    }
}
