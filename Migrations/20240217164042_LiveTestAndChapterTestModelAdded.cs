using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class LiveTestAndChapterTestModelAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChapterTestModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instruction = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterTestModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterTestModel_TopicModel_TopicId",
                        column: x => x.TopicId,
                        principalTable: "TopicModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LiveTestModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instruction = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TestDuration = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveTestModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiveTestModel_SubjectModel_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SubjectModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChapterTestQuestionModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Question = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuestionImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Solution = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SolutionImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NegativeMark = table.Column<int>(type: "int", nullable: false),
                    PositiveMark = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    ChapterTestModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterTestQuestionModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterTestQuestionModel_ChapterTestModel_ChapterTestModelId",
                        column: x => x.ChapterTestModelId,
                        principalTable: "ChapterTestModel",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LiveTestQuestionModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Question = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuestionImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Solution = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SolutionImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NegativeMark = table.Column<int>(type: "int", nullable: false),
                    PositiveMark = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    LiveTestModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveTestQuestionModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiveTestQuestionModel_LiveTestModel_LiveTestModelId",
                        column: x => x.LiveTestModelId,
                        principalTable: "LiveTestModel",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChapterTestOptionModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Option = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OptionImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChapterTestQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterTestOptionModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterTestOptionModel_ChapterTestQuestionModel_ChapterTestQ~",
                        column: x => x.ChapterTestQuestionId,
                        principalTable: "ChapterTestQuestionModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LiveTestOptionModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Option = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OptionImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LiveTestQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveTestOptionModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiveTestOptionModel_LiveTestQuestionModel_LiveTestQuestionId",
                        column: x => x.LiveTestQuestionId,
                        principalTable: "LiveTestQuestionModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestModel_TopicId",
                table: "ChapterTestModel",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestOptionModel_ChapterTestQuestionId",
                table: "ChapterTestOptionModel",
                column: "ChapterTestQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestQuestionModel_ChapterTestModelId",
                table: "ChapterTestQuestionModel",
                column: "ChapterTestModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTestModel_SubjectId",
                table: "LiveTestModel",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTestOptionModel_LiveTestQuestionId",
                table: "LiveTestOptionModel",
                column: "LiveTestQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTestQuestionModel_LiveTestModelId",
                table: "LiveTestQuestionModel",
                column: "LiveTestModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChapterTestOptionModel");

            migrationBuilder.DropTable(
                name: "LiveTestOptionModel");

            migrationBuilder.DropTable(
                name: "ChapterTestQuestionModel");

            migrationBuilder.DropTable(
                name: "LiveTestQuestionModel");

            migrationBuilder.DropTable(
                name: "ChapterTestModel");

            migrationBuilder.DropTable(
                name: "LiveTestModel");
        }
    }
}
