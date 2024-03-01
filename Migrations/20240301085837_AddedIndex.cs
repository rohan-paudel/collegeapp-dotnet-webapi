using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestQuestionModel_ChapterTestId_Status",
                table: "ChapterTestQuestionModel",
                columns: new[] { "ChapterTestId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestQuestionModel_Status",
                table: "ChapterTestQuestionModel",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChapterTestQuestionModel_ChapterTestId_Status",
                table: "ChapterTestQuestionModel");

            migrationBuilder.DropIndex(
                name: "IX_ChapterTestQuestionModel_Status",
                table: "ChapterTestQuestionModel");
        }
    }
}
