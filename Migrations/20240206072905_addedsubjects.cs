using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Addedsubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .AddColumn<string>(
                    name: "SubjectId",
                    table: "SubCourseModel",
                    type: "longtext",
                    nullable: true
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "SubjectModel",
                    columns: table =>
                        new
                        {
                            Id = table
                                .Column<string>(type: "varchar(255)", nullable: false)
                                .Annotation("MySql:CharSet", "utf8mb4"),
                            Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                            Name = table
                                .Column<string>(type: "longtext", nullable: false)
                                .Annotation("MySql:CharSet", "utf8mb4"),
                            SubCourseId = table
                                .Column<string>(type: "longtext", nullable: true)
                                .Annotation("MySql:CharSet", "utf8mb4")
                        },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_SubjectModel", x => x.Id);
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "SubCourseModelSubjectModel",
                    columns: table =>
                        new
                        {
                            SubCoursesId = table
                                .Column<string>(type: "varchar(255)", nullable: false)
                                .Annotation("MySql:CharSet", "utf8mb4"),
                            SubjectsId = table
                                .Column<string>(type: "varchar(255)", nullable: false)
                                .Annotation("MySql:CharSet", "utf8mb4")
                        },
                    constraints: table =>
                    {
                        table.PrimaryKey(
                            "PK_SubCourseModelSubjectModel",
                            x => new { x.SubCoursesId, x.SubjectsId }
                        );
                        table.ForeignKey(
                            name: "FK_SubCourseModelSubjectModel_SubCourseModel_SubCoursesId",
                            column: x => x.SubCoursesId,
                            principalTable: "SubCourseModel",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade
                        );
                        table.ForeignKey(
                            name: "FK_SubCourseModelSubjectModel_SubjectModel_SubjectsId",
                            column: x => x.SubjectsId,
                            principalTable: "SubjectModel",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade
                        );
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SubCourseModelSubjectModel_SubjectsId",
                table: "SubCourseModelSubjectModel",
                column: "SubjectsId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "SubCourseModelSubjectModel");

            migrationBuilder.DropTable(name: "SubjectModel");

            migrationBuilder.DropColumn(name: "SubjectId", table: "SubCourseModel");
        }
    }
}
