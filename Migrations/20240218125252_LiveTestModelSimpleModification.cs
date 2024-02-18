using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class LiveTestModelSimpleModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiveTestQuestionModel_LiveTestModel_LiveTestModelId",
                table: "LiveTestQuestionModel");

            migrationBuilder.DropIndex(
                name: "IX_LiveTestQuestionModel_LiveTestModelId",
                table: "LiveTestQuestionModel");

            migrationBuilder.DropColumn(
                name: "LiveTestModelId",
                table: "LiveTestQuestionModel");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "LiveTestQuestionModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LiveTestId",
                table: "LiveTestQuestionModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "LiveTestOptionModel",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LiveTestModel",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ChapterTestModel",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTestQuestionModel_LiveTestId",
                table: "LiveTestQuestionModel",
                column: "LiveTestId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTestModel_Name",
                table: "LiveTestModel",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTestModel_Status",
                table: "LiveTestModel",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestModel_Name",
                table: "ChapterTestModel",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_LiveTestQuestionModel_LiveTestModel_LiveTestId",
                table: "LiveTestQuestionModel",
                column: "LiveTestId",
                principalTable: "LiveTestModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiveTestQuestionModel_LiveTestModel_LiveTestId",
                table: "LiveTestQuestionModel");

            migrationBuilder.DropIndex(
                name: "IX_LiveTestQuestionModel_LiveTestId",
                table: "LiveTestQuestionModel");

            migrationBuilder.DropIndex(
                name: "IX_LiveTestModel_Name",
                table: "LiveTestModel");

            migrationBuilder.DropIndex(
                name: "IX_LiveTestModel_Status",
                table: "LiveTestModel");

            migrationBuilder.DropIndex(
                name: "IX_ChapterTestModel_Name",
                table: "ChapterTestModel");

            migrationBuilder.DropColumn(
                name: "LiveTestId",
                table: "LiveTestQuestionModel");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "LiveTestOptionModel");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "LiveTestQuestionModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LiveTestModelId",
                table: "LiveTestQuestionModel",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LiveTestModel",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ChapterTestModel",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTestQuestionModel_LiveTestModelId",
                table: "LiveTestQuestionModel",
                column: "LiveTestModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_LiveTestQuestionModel_LiveTestModel_LiveTestModelId",
                table: "LiveTestQuestionModel",
                column: "LiveTestModelId",
                principalTable: "LiveTestModel",
                principalColumn: "Id");
        }
    }
}
