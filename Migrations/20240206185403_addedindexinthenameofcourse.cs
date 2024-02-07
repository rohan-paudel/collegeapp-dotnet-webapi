using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Addedindexinthenameofcourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_CourseModel_Status", table: "CourseModel");

            migrationBuilder
                .AlterColumn<string>(
                    name: "Name",
                    table: "CourseModel",
                    type: "varchar(255)",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "longtext"
                )
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModel_Status_Name",
                table: "CourseModel",
                columns: new[] { "Status", "Name" }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_CourseModel_Status_Name", table: "CourseModel");

            migrationBuilder
                .AlterColumn<string>(
                    name: "Name",
                    table: "CourseModel",
                    type: "longtext",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "varchar(255)"
                )
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModel_Status",
                table: "CourseModel",
                column: "Status"
            );
        }
    }
}
