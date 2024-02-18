using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ImprovementInChapterTestOptionsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChapterTestQuestionModel_ChapterTestModel_ChapterTestModelId",
                table: "ChapterTestQuestionModel");

            migrationBuilder.DropIndex(
                name: "IX_ChapterTestQuestionModel_ChapterTestModelId",
                table: "ChapterTestQuestionModel");

            migrationBuilder.DropColumn(
                name: "ChapterTestModelId",
                table: "ChapterTestQuestionModel");

            migrationBuilder.AddColumn<int>(
                name: "ChapterTestId",
                table: "ChapterTestQuestionModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestQuestionModel_ChapterTestId",
                table: "ChapterTestQuestionModel",
                column: "ChapterTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestModel_Status",
                table: "ChapterTestModel",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_ChapterTestQuestionModel_ChapterTestModel_ChapterTestId",
                table: "ChapterTestQuestionModel",
                column: "ChapterTestId",
                principalTable: "ChapterTestModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChapterTestQuestionModel_ChapterTestModel_ChapterTestId",
                table: "ChapterTestQuestionModel");

            migrationBuilder.DropIndex(
                name: "IX_ChapterTestQuestionModel_ChapterTestId",
                table: "ChapterTestQuestionModel");

            migrationBuilder.DropIndex(
                name: "IX_ChapterTestModel_Status",
                table: "ChapterTestModel");

            migrationBuilder.DropColumn(
                name: "ChapterTestId",
                table: "ChapterTestQuestionModel");

            migrationBuilder.AddColumn<int>(
                name: "ChapterTestModelId",
                table: "ChapterTestQuestionModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestQuestionModel_ChapterTestModelId",
                table: "ChapterTestQuestionModel",
                column: "ChapterTestModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChapterTestQuestionModel_ChapterTestModel_ChapterTestModelId",
                table: "ChapterTestQuestionModel",
                column: "ChapterTestModelId",
                principalTable: "ChapterTestModel",
                principalColumn: "Id");
        }
    }
}
