using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedNiceCourseID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SubCourseModel",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SubCourseModel_Name",
                table: "SubCourseModel",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SubCourseModel_Status",
                table: "SubCourseModel",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubCourseModel_Name",
                table: "SubCourseModel");

            migrationBuilder.DropIndex(
                name: "IX_SubCourseModel_Status",
                table: "SubCourseModel");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SubCourseModel",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
