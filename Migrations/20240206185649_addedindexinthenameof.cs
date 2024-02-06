using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class addedindexinthenameof : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseModel_Status_Name",
                table: "CourseModel");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModel_Name",
                table: "CourseModel",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModel_Status",
                table: "CourseModel",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseModel_Name",
                table: "CourseModel");

            migrationBuilder.DropIndex(
                name: "IX_CourseModel_Status",
                table: "CourseModel");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModel_Status_Name",
                table: "CourseModel",
                columns: new[] { "Status", "Name" });
        }
    }
}
