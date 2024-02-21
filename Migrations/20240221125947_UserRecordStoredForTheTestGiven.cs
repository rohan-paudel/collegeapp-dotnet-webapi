using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UserRecordStoredForTheTestGiven : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "PositiveMark",
                table: "ChapterTestQuestionModel",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "NegativeMark",
                table: "ChapterTestQuestionModel",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ChapterTestUserDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StudentId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChapterTestId = table.Column<int>(type: "int", nullable: false),
                    Correct = table.Column<int>(type: "int", nullable: false),
                    Incorrect = table.Column<int>(type: "int", nullable: false),
                    Unanswered = table.Column<int>(type: "int", nullable: false),
                    MarksObtained = table.Column<float>(type: "float", nullable: false),
                    TotalMark = table.Column<float>(type: "float", nullable: false),
                    TotalQuestion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterTestUserDataModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterTestUserDataModel_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChapterTestUserDataModel_ChapterTestModel_ChapterTestId",
                        column: x => x.ChapterTestId,
                        principalTable: "ChapterTestModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChapterTestDetailedDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ChapterTestQuestionId = table.Column<int>(type: "int", nullable: false),
                    UserAnswerId = table.Column<int>(type: "int", nullable: true),
                    ChapterTestUserDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterTestDetailedDataModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterTestDetailedDataModel_ChapterTestOptionModel_UserAnsw~",
                        column: x => x.UserAnswerId,
                        principalTable: "ChapterTestOptionModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChapterTestDetailedDataModel_ChapterTestQuestionModel_Chapte~",
                        column: x => x.ChapterTestQuestionId,
                        principalTable: "ChapterTestQuestionModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChapterTestDetailedDataModel_ChapterTestUserDataModel_Chapte~",
                        column: x => x.ChapterTestUserDataId,
                        principalTable: "ChapterTestUserDataModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestDetailedDataModel_ChapterTestQuestionId",
                table: "ChapterTestDetailedDataModel",
                column: "ChapterTestQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestDetailedDataModel_ChapterTestUserDataId",
                table: "ChapterTestDetailedDataModel",
                column: "ChapterTestUserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestDetailedDataModel_UserAnswerId",
                table: "ChapterTestDetailedDataModel",
                column: "UserAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestUserDataModel_ChapterTestId",
                table: "ChapterTestUserDataModel",
                column: "ChapterTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTestUserDataModel_StudentId",
                table: "ChapterTestUserDataModel",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChapterTestDetailedDataModel");

            migrationBuilder.DropTable(
                name: "ChapterTestUserDataModel");

            migrationBuilder.AlterColumn<int>(
                name: "PositiveMark",
                table: "ChapterTestQuestionModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "NegativeMark",
                table: "ChapterTestQuestionModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }
    }
}
