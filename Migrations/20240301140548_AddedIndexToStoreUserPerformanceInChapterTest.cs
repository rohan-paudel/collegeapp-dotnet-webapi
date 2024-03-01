using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndexToStoreUserPerformanceInChapterTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestUserDataModel_StudentId_ChapterTestId",
                table: "ChapterTestUserDataModel",
                columns: new[] { "StudentId", "ChapterTestId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChapterTestUserDataModel_StudentId_ChapterTestId",
                table: "ChapterTestUserDataModel");
        }
    }
}
